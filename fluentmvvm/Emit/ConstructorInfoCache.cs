using System;
using System.Reflection;

namespace FluentMvvm.Emit
{
    /// <summary>
    ///     Caches frequently used <see cref="ConstructorInfo" />s so that they are not retrieved every time.
    /// </summary>
    internal static class ConstructorInfoCache
    {
        /// <summary>
        ///     The <see cref="System.InvalidOperationException(string)" /> constructor.
        /// </summary>
        public static readonly ConstructorInfo InvalidOperationException = typeof(InvalidOperationException).GetConstructor(new[] { typeof(string) });

        /// <summary>
        ///     The <see cref="System.ArgumentException(string)" /> constructor.
        /// </summary>
        public static readonly ConstructorInfo ArgumentException = typeof(ArgumentException).GetConstructor(new[] { typeof(string) });

        /// <summary>
        ///     The <see cref="System.InvalidCastException(string)" /> constructor.
        /// </summary>
        public static readonly ConstructorInfo InvalidCastException = typeof(InvalidCastException).GetConstructor(new[] { typeof(string) });
    }
}