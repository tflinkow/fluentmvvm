using System;
using FluentMvvm.Fluent;

namespace FluentMvvm.Emit
{
    /// <inheritdoc />
    /// <summary>
    ///     The base interface for dynamically generated types containing the backing fields of a view model's writable
    ///     public instance properties.
    /// </summary>
    internal interface IBackingFieldProvider : IPropertyGetExpression
    {
        /// <summary>
        ///     Sets the value of the specified field to <paramref name="value" /> and returns <c>true</c> if the new value was
        ///     different from the old value.
        /// </summary>
        /// <typeparam name="T">The type of the field.</typeparam>
        /// <param name="value">The value to set the field to.</param>
        /// <param name="propertyName">The name of the backing field, which is the same as the name of the property it backs.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> could
        ///     be found -or- <paramref name="propertyName" /> is empty or consists only of white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        bool Set<T>(T value, string propertyName);

        /// <summary>
        ///     Sets the value of the specified field to <paramref name="value" /> and returns <c>true</c> if the new value was
        ///     different from the old value.
        /// </summary>
        /// <param name="value">The value to set the field to.</param>
        /// <param name="propertyName">The name of the backing field, which is the same as the name of the property it backs.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> could
        ///     be found -or- <paramref name="propertyName" /> is empty or consists only of white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        bool Set(bool value, string propertyName);

        /// <summary>
        ///     Sets the value of the specified field to <paramref name="value" /> and returns <c>true</c> if the new value was
        ///     different from the old value.
        /// </summary>
        /// <param name="value">The value to set the field to.</param>
        /// <param name="propertyName">The name of the backing field, which is the same as the name of the property it backs.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> could
        ///     be found -or- <paramref name="propertyName" /> is empty or consists only of white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        bool Set(byte value, string propertyName);

        /// <summary>
        ///     Sets the value of the specified field to <paramref name="value" /> and returns <c>true</c> if the new value was
        ///     different from the old value.
        /// </summary>
        /// <param name="value">The value to set the field to.</param>
        /// <param name="propertyName">The name of the backing field, which is the same as the name of the property it backs.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> could
        ///     be found -or- <paramref name="propertyName" /> is empty or consists only of white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        bool Set(sbyte value, string propertyName);

        /// <summary>
        ///     Sets the value of the specified field to <paramref name="value" /> and returns <c>true</c> if the new value was
        ///     different from the old value.
        /// </summary>
        /// <param name="value">The value to set the field to.</param>
        /// <param name="propertyName">The name of the backing field, which is the same as the name of the property it backs.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> could
        ///     be found -or- <paramref name="propertyName" /> is empty or consists only of white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        bool Set(char value, string propertyName);

        /// <summary>
        ///     Sets the value of the specified field to <paramref name="value" /> and returns <c>true</c> if the new value was
        ///     different from the old value.
        /// </summary>
        /// <param name="value">The value to set the field to.</param>
        /// <param name="propertyName">The name of the backing field, which is the same as the name of the property it backs.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> could
        ///     be found -or- <paramref name="propertyName" /> is empty or consists only of white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        bool Set(double value, string propertyName);

        /// <summary>
        ///     Sets the value of the specified field to <paramref name="value" /> and returns <c>true</c> if the new value was
        ///     different from the old value.
        /// </summary>
        /// <param name="value">The value to set the field to.</param>
        /// <param name="propertyName">The name of the backing field, which is the same as the name of the property it backs.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> could
        ///     be found -or- <paramref name="propertyName" /> is empty or consists only of white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        bool Set(float value, string propertyName);

        /// <summary>
        ///     Sets the value of the specified field to <paramref name="value" /> and returns <c>true</c> if the new value was
        ///     different from the old value.
        /// </summary>
        /// <param name="value">The value to set the field to.</param>
        /// <param name="propertyName">The name of the backing field, which is the same as the name of the property it backs.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> could
        ///     be found -or- <paramref name="propertyName" /> is empty or consists only of white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        bool Set(short value, string propertyName);

        /// <summary>
        ///     Sets the value of the specified field to <paramref name="value" /> and returns <c>true</c> if the new value was
        ///     different from the old value.
        /// </summary>
        /// <param name="value">The value to set the field to.</param>
        /// <param name="propertyName">The name of the backing field, which is the same as the name of the property it backs.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> could
        ///     be found -or- <paramref name="propertyName" /> is empty or consists only of white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        bool Set(ushort value, string propertyName);

        /// <summary>
        ///     Sets the value of the specified field to <paramref name="value" /> and returns <c>true</c> if the new value was
        ///     different from the old value.
        /// </summary>
        /// <param name="value">The value to set the field to.</param>
        /// <param name="propertyName">The name of the backing field, which is the same as the name of the property it backs.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> could
        ///     be found -or- <paramref name="propertyName" /> is empty or consists only of white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        bool Set(int value, string propertyName);

        /// <summary>
        ///     Sets the value of the specified field to <paramref name="value" /> and returns <c>true</c> if the new value was
        ///     different from the old value.
        /// </summary>
        /// <param name="value">The value to set the field to.</param>
        /// <param name="propertyName">The name of the backing field, which is the same as the name of the property it backs.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> could
        ///     be found -or- <paramref name="propertyName" /> is empty or consists only of white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        bool Set(uint value, string propertyName);

        /// <summary>
        ///     Sets the value of the specified field to <paramref name="value" /> and returns <c>true</c> if the new value was
        ///     different from the old value.
        /// </summary>
        /// <param name="value">The value to set the field to.</param>
        /// <param name="propertyName">The name of the backing field, which is the same as the name of the property it backs.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> could
        ///     be found -or- <paramref name="propertyName" /> is empty or consists only of white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        bool Set(long value, string propertyName);

        /// <summary>
        ///     Sets the value of the specified field to <paramref name="value" /> and returns <c>true</c> if the new value was
        ///     different from the old value.
        /// </summary>
        /// <param name="value">The value to set the field to.</param>
        /// <param name="propertyName">The name of the backing field, which is the same as the name of the property it backs.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> could
        ///     be found -or- <paramref name="propertyName" /> is empty or consists only of white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        bool Set(ulong value, string propertyName);

        /// <summary>
        ///     Sets the value of the specified field to <paramref name="value" /> and returns <c>true</c> if the new value was
        ///     different from the old value.
        /// </summary>
        /// <param name="value">The value to set the field to.</param>
        /// <param name="propertyName">The name of the backing field, which is the same as the name of the property it backs.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> could
        ///     be found -or- <paramref name="propertyName" /> is empty or consists only of white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        bool Set(string value, string propertyName);

        /// <summary>
        ///     Sets the value of the specified field to <paramref name="value" /> and returns <c>true</c> if the new value was
        ///     different from the old value.
        /// </summary>
        /// <param name="value">The value to set the field to.</param>
        /// <param name="propertyName">The name of the backing field, which is the same as the name of the property it backs.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> could
        ///     be found -or- <paramref name="propertyName" /> is empty or consists only of white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        bool Set(decimal value, string propertyName);

        /// <summary>
        ///     Sets the value of the specified field to <paramref name="value" /> and returns <c>true</c> if the new value was
        ///     different from the old value.
        /// </summary>
        /// <param name="value">The value to set the field to.</param>
        /// <param name="propertyName">The name of the backing field, which is the same as the name of the property it backs.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> could
        ///     be found -or- <paramref name="propertyName" /> is empty or consists only of white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        bool Set(DateTime value, string propertyName);
    }
}