using System;

namespace FluentMvvm.Fluent
{
    /// <summary>
    ///     Provides methods that can be used to get the value of a property.
    /// </summary>
    internal interface IPropertyGetExpression
    {
        /// <summary>
        ///     Gets the value of the specified property.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> could
        ///     be found -or- <paramref name="propertyName" /> is empty or consists only of white-space characters.
        /// </exception>
        /// <exception cref="InvalidCastException">
        ///     Unable to cast the actual type of the property to the specified type
        ///     <typeparamref name="T" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        /// <remarks>
        ///     It is advised to use overloads such as <see cref="GetInt32" /> and <see cref="GetString" /> etc. (where they exist)
        ///     for better performance.
        /// </remarks>
        T Get<T>(string propertyName = "");

        /// <summary>
        ///     Gets the value of the specified property.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> of type
        ///     <see cref="bool" /> could be found -or- <paramref name="propertyName" /> is empty or consists only of
        ///     white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        /// <remarks>
        ///     Intended to only be used in property <c>get</c> accessors.
        /// </remarks>
        bool GetBool(string propertyName = "");

        /// <summary>
        ///     Gets the value of the specified property.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> of type
        ///     <see cref="byte" /> could be found -or- <paramref name="propertyName" /> is empty or consists only of
        ///     white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        /// <remarks>
        ///     Intended to only be used in property <c>get</c> accessors.
        /// </remarks>
        byte GetByte(string propertyName = "");

        /// <summary>
        ///     Gets the value of the specified property.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> of type
        ///     <see cref="char" /> could be found -or- <paramref name="propertyName" /> is empty or consists only of
        ///     white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        /// <remarks>
        ///     Intended to only be used in property <c>get</c> accessors.
        /// </remarks>
        char GetChar(string propertyName = "");

        /// <summary>
        ///     Gets the value of the specified property.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> of type
        ///     <see cref="DateTime" /> could be found -or- <paramref name="propertyName" /> is empty or consists only of
        ///     white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        /// <remarks>
        ///     Intended to only be used in property <c>get</c> accessors.
        /// </remarks>
        DateTime GetDateTime(string propertyName = "");

        /// <summary>
        ///     Gets the value of the specified property.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> of type
        ///     <see cref="decimal" /> could be found -or- <paramref name="propertyName" /> is empty or consists only of
        ///     white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        /// <remarks>
        ///     Intended to only be used in property <c>get</c> accessors.
        /// </remarks>
        decimal GetDecimal(string propertyName = "");

        /// <summary>
        ///     Gets the value of the specified property.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> of type
        ///     <see cref="double" /> could be found -or- <paramref name="propertyName" /> is empty or consists only of
        ///     white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        /// <remarks>
        ///     Intended to only be used in property <c>get</c> accessors.
        /// </remarks>
        double GetDouble(string propertyName = "");

        /// <summary>
        ///     Gets the value of the specified property.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> of type
        ///     <see cref="float" /> could be found -or- <paramref name="propertyName" /> is empty or consists only of
        ///     white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        /// <remarks>
        ///     Intended to only be used in property <c>get</c> accessors.
        /// </remarks>
        float GetFloat(string propertyName = "");

        /// <summary>
        ///     Gets the value of the specified property.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> of type
        ///     <see cref="short" /> could be found -or- <paramref name="propertyName" /> is empty or consists only of
        ///     white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        /// <remarks>
        ///     Intended to only be used in property <c>get</c> accessors.
        /// </remarks>
        short GetInt16(string propertyName = "");

        /// <summary>
        ///     Gets the value of the specified property.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> of type
        ///     <see cref="int" /> could be found -or- <paramref name="propertyName" /> is empty or consists only of
        ///     white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        /// <remarks>
        ///     Intended to only be used in property <c>get</c> accessors.
        /// </remarks>
        int GetInt32(string propertyName = "");

        /// <summary>
        ///     Gets the value of the specified property.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> of type
        ///     <see cref="long" /> could be found -or- <paramref name="propertyName" /> is empty or consists only of
        ///     white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        /// <remarks>
        ///     Intended to only be used in property <c>get</c> accessors.
        /// </remarks>
        long GetInt64(string propertyName = "");

        /// <summary>
        ///     Gets the value of the specified property.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> of type
        ///     <see cref="sbyte" /> could be found -or- <paramref name="propertyName" /> is empty or consists only of
        ///     white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        /// <remarks>
        ///     Intended to only be used in property <c>get</c> accessors.
        /// </remarks>
        sbyte GetSByte(string propertyName = "");

        /// <summary>
        ///     Gets the value of the specified property.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> of type
        ///     <see cref="string" /> could be found -or- <paramref name="propertyName" /> is empty or consists only of
        ///     white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        /// <remarks>
        ///     Intended to only be used in property <c>get</c> accessors.
        /// </remarks>
        string GetString(string propertyName = "");

        /// <summary>
        ///     Gets the value of the specified property.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> of type
        ///     <see cref="ushort" /> could be found -or- <paramref name="propertyName" /> is empty or consists only of
        ///     white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        /// <remarks>
        ///     Intended to only be used in property <c>get</c> accessors.
        /// </remarks>
        ushort GetUInt16(string propertyName = "");

        /// <summary>
        ///     Gets the value of the specified property.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> of type
        ///     <see cref="uint" /> could be found -or- <paramref name="propertyName" /> is empty or consists only of
        ///     white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        /// <remarks>
        ///     Intended to only be used in property <c>get</c> accessors.
        /// </remarks>
        uint GetUInt32(string propertyName = "");

        /// <summary>
        ///     Gets the value of the specified property.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> of type
        ///     <see cref="ulong" /> could be found -or- <paramref name="propertyName" /> is empty or consists only of
        ///     white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        /// <remarks>
        ///     Intended to only be used in property <c>get</c> accessors.
        /// </remarks>
        ulong GetUInt64(string propertyName = "");
    }
}