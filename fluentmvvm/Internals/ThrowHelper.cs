using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace FluentMvvm.Internals
{
    /// <summary>
    /// Using the <see cref="ThrowHelper"/> methods allows for callers to be inlined, reduces duplicate CIL code in generic types and ensures consistent exception messages across the library.
    /// </summary>
    internal static class ThrowHelper
    {
        /// <summary>
        /// Throws a <see cref="KeyNotFoundException"/> with a message stating that <paramref name="key"/> was not present in the dictionary.
        /// </summary>
        /// <param name="key">The key that is not present in the dictionary.</param>
        /// <exception cref="KeyNotFoundException"><paramref name="key"/> was not present in the dictionary.</exception>
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowKeyNotFoundException(string key)
        {
            throw new KeyNotFoundException($"The given key '{key}' was not present in the dictionary.");
        }

        /// <summary>
        /// Throws a <see cref="InvalidOperationException"/> with a message stating that <paramref name="key"/> was already present in the dictionary.
        /// </summary>
        /// <param name="key">The key that is already present in the dictionary.</param>
        /// <exception cref="InvalidOperationException"><paramref name="key"/> was already present in the dictionary.</exception>
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowDuplicateKeyException(string key)
        {
            throw new InvalidOperationException($"Cannot add value with key '{key}' because an entry with the same key already exists.");
        }

        /// <summary>
        /// Throws a <see cref="ArgumentException"/> with a message stating that <paramref name="name"/> must not be <c>null</c>, empty or consist only of white-space characters.
        /// </summary>
        /// <param name="name">The name of the parameter that is <c>null</c>, empty or consists only of white-space characters.</param>
        /// <exception cref="ArgumentException">the parameter is <c>null</c>, empty or consists only of white-space characters.</exception>
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowArgumentNullEmptyOrWhiteSpace(string name)
        {
            throw new ArgumentException($"{name} must not be null, empty or consist only of white-space characters.", name);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="name">The name of the parameter that is <c>null</c>.</param>
        /// <exception cref="ArgumentNullException">the parameter is <c>null</c>.</exception>
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowArgumentNullException(string name)
        {
            throw new ArgumentNullException(name);
        }

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> stating that no backing fields of type <typeparamref name="T"/> were found on type <paramref name="onType"/>.
        /// </summary>
        /// <typeparam name="T">The type of the backing fields.</typeparam>
        /// <param name="onType">The type to search for the fields on.</param>
        /// <exception cref="InvalidOperationException">there are no backing fields of type <typeparamref name="T"/> on the type <paramref name="onType"/>.</exception>
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowNoBackingFieldsOfType<T>(Type onType)
        {
            throw new InvalidOperationException($"There are no backing fields of type '{typeof(T)}' on type '{onType}'. Make sure to enable backing field creation in the FluentViewModelBase constructor.");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> stating that no public writable instance property named <paramref name="propertyName"/> was found on type <paramref name="onType"/>.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="propertyType">The type of the property.</param>
        /// <param name="onType">The type to search for the property on</param>
        /// <exception cref="ArgumentException">there is no public writable instance property named <paramref name="propertyName"/> on type <paramref name="onType"/>.</exception>
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowPropertyNotFound(string propertyName, Type propertyType, Type onType)
        {
            throw new ArgumentException($"Cannot find a public writable instance property named '{propertyName}' of type '{propertyType}' on type '{onType}'.");
        }
    }
}