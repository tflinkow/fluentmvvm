using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FluentMvvm.Internals
{
#if TEST
    internal class FixedSizeDictionary<TValue>
#else
    /// <summary>
    ///     Provides a fixed-size read-only dictionary.
    /// </summary>
    /// <remarks>No values can be added or removed after creation, but the values itself are mutable.</remarks>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    internal sealed class FixedSizeDictionary<TValue>
#endif
    {
        private readonly Entry[] buckets;
        private readonly int indexMask;

#if TEST
        public int Count;
#else
        /// <summary>
        ///     Gets the number of elements in the collection.
        /// </summary>
        private int Count;
#endif

#if TEST
        public FixedSizeDictionary()
        {
            this.buckets = default!;
        }
#endif

#if TEST
        internal FixedSizeDictionary(int bucketSize)
#else
        /// <summary>
        ///     Initializes a new instance of the <see cref="FixedSizeDictionary{TValue}" /> class.
        /// </summary>
        /// <param name="bucketSize">The number of <see cref="Entry" />s to create as the backing array.</param>
        private FixedSizeDictionary(int bucketSize)
#endif
        {
            Debug.Assert(bucketSize >= 0);

            this.buckets = bucketSize is 0 ? Array.Empty<Entry>() : new Entry[bucketSize];
            this.indexMask = this.buckets.Length - 1;
        }

#if TEST
        public virtual bool TryGetEntry(string key, [NotNullWhen(true)] out Entry? entry)
#else
        /// <summary>
        ///     Gets the entry associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="entry">
        ///     When this method returns <c>true</c>, contains the entry associated with the specified key, if the
        ///     key is found; otherwise, <c>null</c>. This parameter is passed uninitialized.
        /// </param>
        /// <returns><c>true</c>, if <paramref name="key" /> exists in the dictionary; otherwise, <c>false</c>.</returns>
        public bool TryGetEntry([MaybeNullWhen(false)] string key, [NotNullWhen(true)] out Entry? entry)
#endif
        {
            // This method is performance-critical as every Get and Set method calls it.
            // We should check whether key is null, empty or white-space but as we know that in almost all cases the compiler will supply the value, we skip the check here.
            // If the key is null, empty or white-space we return false to the caller. The caller should then check whether key was null, empty or white-space.

            switch (this.Count)
            {
                case 0:
                    goto unsuccessful;
                case 1:
                    Debug.Assert(!String.IsNullOrWhiteSpace(this.buckets[0].Key), "buckets[0].Key should not be null, empty or white-space as this should be enforced in the Add method.");
                    RuntimeHelper.AssertInterned(this.buckets[0].Key);

                    // It does not make sense to calculate and compare hashes because here we need to check for equality anyways.
                    if (this.buckets[0].Key == key)
                    {
                        RuntimeHelper.AssertInterned(key);

                        entry = this.buckets[0];
                        return true;
                    }

                    // buckets[0].Key is not null, empty or white-space. If key is, we simply return false (no explicit checking here).
                    goto unsuccessful;
            }

            // This null check cannot be avoided. Let the caller figure out the problem by returning false.
            if (key is null)
            {
                goto unsuccessful;
            }

            int hash = key.GetHashCode();
            Entry? next = this.buckets[this.DetermineIndex(hash)];

            while (next != null)
            {
                Debug.Assert(!String.IsNullOrWhiteSpace(next.Key), "no entry should have a null, empty or white-space key as this should be enforced in the Add method.");
                RuntimeHelper.AssertInterned(next.Key);

                if (next.HashCode == hash && next.Key == key)
                {
                    RuntimeHelper.AssertInterned(key);

                    entry = next;
                    return true;
                }

                // if key is null, empty or white-space we continue with the loop.
                // That is okay because we are already in an exceptional situation; performance does not matter anymore.
                // Eventually, we leave the loop (as no entry in the dictionary will have a matching key) and return false to the caller.
                next = next.Next;
            }

            unsuccessful:
            entry = default;
            return false;
        }

        /// <summary>
        ///     Creates a <see cref="FixedSizeDictionary{TValue}" /> from an <see cref="IEnumerable{T}" /> according to specified
        ///     key selector and value selector functions.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <exception cref="InvalidOperationException">An entry with the same key already exists.</exception>
        public static FixedSizeDictionary<TValue> Create<TSource>(IEnumerable<TSource> source, Func<TSource, string> keySelector, Func<TSource, TValue> valueSelector)
        {
            source.NotNull(nameof(source));
            keySelector.NotNull(nameof(keySelector));
            valueSelector.NotNull(nameof(valueSelector));

            TSource[] materializedSource = source.ToArray();
            int bucketSize = FixedSizeDictionary<TValue>.CalculateCapacity(materializedSource.Length);

            var result = new FixedSizeDictionary<TValue>(bucketSize);

            foreach (TSource x in materializedSource)
            {
                result.Add(keySelector(x), valueSelector(x));
            }

            Debug.Assert(result.Count == materializedSource.Length);

            return result;
        }

#if TEST
        [Pure]
        internal static int CalculateCapacity(int collectionSize)
#else
        /// <summary>
        /// Calculates the number of buckets for a collection of size <paramref name="collectionSize"/> so that the load factor does not exceed ln(2).
        /// </summary>
        /// <param name="collectionSize">The number of values to add to the dictionary.</param>
        [Pure]
        private static int CalculateCapacity(int collectionSize)
#endif
        {
            Debug.Assert(collectionSize >= 0);

            switch (collectionSize)
            {
                case 0:
                    return 0;
                case 1:
                    // Saves us an expensive GetHashCode calculation and unnecessary allocations.
                    return 1;
            }

            double loadFactor = Math.Log(2, Math.E);

            // next power of two of x is 2^(ceil(ld(x)))
            // to ensure that the actual load factor does not exceed the desired load factor, we first divide x by the desired load factor.
            var capacity = (int) Math.Pow(2, Math.Ceiling(Math.Log(collectionSize / loadFactor, 2)));

            Debug.Assert(capacity >= 1);
            Debug.Assert((double) collectionSize / capacity <= loadFactor);

            return capacity;
        }

        /// <summary>
        ///     Determines the index in the buckets array for the element with hash code <paramref name="hashCode" />.
        /// </summary>
        /// <param name="hashCode">The hash code of the element.</param>
        /// <returns>The index in the buckets array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int DetermineIndex(int hashCode)
        {
            int index = hashCode & this.indexMask;

            Debug.Assert(index >= 0 && index < this.buckets.Length);

            return index;
        }

        /// <summary>
        ///     Adds the <paramref name="value" /> to the dictionary, if the key <paramref name="key" /> was not already added to
        ///     the dictionary.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="InvalidOperationException">An entry with the key <paramref name="key" /> already exists.</exception>
        private void Add(string key, [AllowNull] TValue value)
        {
            // Ensuring that no null, empty or white-space key is added to the dictionary here so that TryGetEntry works as designed.
            key.NotNullOrEmptyOrWhiteSpace(nameof(key));

            if (this.buckets.Length is 1)
            {
                // It makes no sense to calculate the hash, because when retrieving values we need to check for equality anyway.
                this.buckets[0] = FixedSizeDictionary<TValue>.CreateEntry(key, value, 0);
                this.Count = 1;
                return;
            }

            int hash = key.GetHashCode();
            int index = this.DetermineIndex(hash);

            if (this.buckets[index] is null)
            {
                this.buckets[index] = FixedSizeDictionary<TValue>.CreateEntry(key, value, hash);
            }
            else
            {
                Entry lastEntry = this.buckets[index];

                while (true)
                {
                    if (lastEntry.Key == key)
                    {
                        ThrowHelper.ThrowDuplicateKeyException(key);
                    }

                    if (lastEntry.Next is null)
                    {
                        lastEntry.Next = FixedSizeDictionary<TValue>.CreateEntry(key, value, hash);
                        break;
                    }

                    lastEntry = lastEntry.Next;
                }
            }

            this.Count++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Entry CreateEntry(string key, [AllowNull] TValue value, int hashCode)
        {
            Debug.Assert(!String.IsNullOrWhiteSpace(key));

            return new Entry(RuntimeHelper.AreLiteralsInterned ? String.Intern(key) : key, value, hashCode);
        }

        /// <summary>
        ///     Represents <see cref="FixedSizeDictionary{TValue}" /> entry.
        /// </summary>
        public sealed class Entry
        {
            public readonly int HashCode;
            public readonly string Key;
            public Entry? Next;

            [AllowNull]
            public TValue Value;

            /// <summary>
            ///     Initializes a new instance of the <see cref="Entry" /> class.
            /// </summary>
            /// <param name="key">The key.</param>
            /// <param name="value">The value.</param>
            /// <param name="hashCode">The hash code.</param>
            internal Entry(string key, [AllowNull] TValue value, int hashCode)
            {
                Debug.Assert(!String.IsNullOrWhiteSpace(key));

                this.Key = key;
                this.Value = value;
                this.HashCode = hashCode;
            }

#if TEST
            public Entry()
            {
                this.Key = default!;
                this.Next = default;
            }
#endif
        }
    }
}