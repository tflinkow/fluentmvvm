using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace FluentMvvm.Internals
{
    internal static class ObjectExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if <paramref name="value"/> is <c>null</c>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        /// <returns><paramref name="value"/>, if it is not <c>null</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NotNull<T>([NotNullIfNotNull("value")] [AllowNull] this T value, string parameterName)
        {
            if (value is null)
            {
                ThrowHelper.ThrowArgumentNullException(parameterName);
            }

            return value;
        }

        /// <summary>
        /// Throws a <see cref="ArgumentException"/> if <paramref name="value"/> must not be <c>null</c>, empty or consist only of white-space characters.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"><paramref name="value"/> is <c>null</c>, empty or consists only of white-space characters.</exception>
        /// <returns><paramref name="value"/>, if it is not <c>null</c>, empty or consists only of whitespace characters.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string NotNullOrEmptyOrWhiteSpace([NotNullIfNotNull("value")] this string? value, string parameterName)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                ThrowHelper.ThrowArgumentNullEmptyOrWhiteSpace(parameterName);
            }

            return value!;
        }
    }
}