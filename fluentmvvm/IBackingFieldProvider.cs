using System;

using JetBrains.Annotations;

namespace FluentMvvm
{
    /// <summary>
    ///     The base interface for dynamically generated types containing the backing fields of a view model's writable
    ///     public instance properties.
    /// </summary>
    internal interface IBackingFieldProvider
    {
        /// <summary>Gets the value of backing field of the property <paramref name="propertyName" />.</summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The value of the backing field of <paramref name="propertyName" />.</returns>
        /// <exception cref="ArgumentException">
        ///     no public, writable instance property with the specified property name could be
        ///     found.
        /// </exception>
        object GetValueOf([NotNull] string propertyName);

        /// <summary>
        ///     Sets the value of the backing field of the property <paramref name="propertyName" /> if it differs from the
        ///     current property value.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="value">The value to set to the property.</param>
        /// <returns><c>true</c> if <paramref name="value" /> differed from the old value; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentException">
        ///     no public, writable instance property with the specified property name could be
        ///     found.
        /// </exception>
        bool SetValueOf([NotNull] string propertyName, [CanBeNull] object value);
    }
}