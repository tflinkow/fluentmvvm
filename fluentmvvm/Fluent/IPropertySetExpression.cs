using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FluentMvvm.Fluent
{
    /// <summary>
    ///     Provides methods that can be used to set a property value.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public interface IPropertySetExpression
    {
        /// <summary>
        ///     Sets the value of the specified property to <paramref name="value" /> and raises a
        ///     <see cref="INotifyPropertyChanged.PropertyChanged" /> event if the new value was different from the old value.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///     No public writable instance property named <paramref name="propertyName" /> could
        ///     be found -or- <paramref name="propertyName" /> is empty or consists only of white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The type is marked with <see cref="SuppressFieldGenerationAttribute" />.
        ///     This method cannot be used when field generation is suppressed.
        /// </exception>
        /// <remarks>
        ///     Intended to only be used in property <c>set</c> accessors.
        /// </remarks>
        IDependencyExpression Set<T>(T value, [CallerMemberName] string propertyName = "");

        /// <summary>
        ///     Sets the value of this property to <paramref name="value" /> and raises a
        ///     <see cref="INotifyPropertyChanged.PropertyChanged" /> event if the new value was different from the old value.
        /// </summary>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns></returns>
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
        ///     Intended to only be used in property <c>set</c> accessors.
        /// </remarks>
        IDependencyExpression Set(bool value, [CallerMemberName] string propertyName = "");

        /// <summary>
        ///     Sets the value of this property to <paramref name="value" /> and raises a
        ///     <see cref="INotifyPropertyChanged.PropertyChanged" /> event if the new value was different from the old value.
        /// </summary>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns></returns>
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
        ///     Intended to only be used in property <c>set</c> accessors.
        /// </remarks>
        IDependencyExpression Set(byte value, [CallerMemberName] string propertyName = "");

        /// <summary>
        ///     Sets the value of this property to <paramref name="value" /> and raises a
        ///     <see cref="INotifyPropertyChanged.PropertyChanged" /> event if the new value was different from the old value.
        /// </summary>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns></returns>
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
        ///     Intended to only be used in property <c>set</c> accessors.
        /// </remarks>
        IDependencyExpression Set(sbyte value, [CallerMemberName] string propertyName = "");

        /// <summary>
        ///     Sets the value of this property to <paramref name="value" /> and raises a
        ///     <see cref="INotifyPropertyChanged.PropertyChanged" /> event if the new value was different from the old value.
        /// </summary>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns></returns>
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
        ///     Intended to only be used in property <c>set</c> accessors.
        /// </remarks>
        IDependencyExpression Set(char value, [CallerMemberName] string propertyName = "");

        /// <summary>
        ///     Sets the value of this property to <paramref name="value" /> and raises a
        ///     <see cref="INotifyPropertyChanged.PropertyChanged" /> event if the new value was different from the old value.
        /// </summary>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns></returns>
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
        ///     Intended to only be used in property <c>set</c> accessors.
        /// </remarks>
        IDependencyExpression Set(decimal value, [CallerMemberName] string propertyName = "");

        /// <summary>
        ///     Sets the value of this property to <paramref name="value" /> and raises a
        ///     <see cref="INotifyPropertyChanged.PropertyChanged" /> event if the new value was different from the old value.
        /// </summary>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns></returns>
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
        ///     Intended to only be used in property <c>set</c> accessors.
        /// </remarks>
        IDependencyExpression Set(double value, [CallerMemberName] string propertyName = "");

        /// <summary>
        ///     Sets the value of this property to <paramref name="value" /> and raises a
        ///     <see cref="INotifyPropertyChanged.PropertyChanged" /> event if the new value was different from the old value.
        /// </summary>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns></returns>
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
        ///     Intended to only be used in property <c>set</c> accessors.
        /// </remarks>
        IDependencyExpression Set(float value, [CallerMemberName] string propertyName = "");

        /// <summary>
        ///     Sets the value of this property to <paramref name="value" /> and raises a
        ///     <see cref="INotifyPropertyChanged.PropertyChanged" /> event if the new value was different from the old value.
        /// </summary>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns></returns>
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
        ///     Intended to only be used in property <c>set</c> accessors.
        /// </remarks>
        IDependencyExpression Set(short value, [CallerMemberName] string propertyName = "");

        /// <summary>
        ///     Sets the value of this property to <paramref name="value" /> and raises a
        ///     <see cref="INotifyPropertyChanged.PropertyChanged" /> event if the new value was different from the old value.
        /// </summary>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns></returns>
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
        ///     Intended to only be used in property <c>set</c> accessors.
        /// </remarks>
        IDependencyExpression Set(ushort value, [CallerMemberName] string propertyName = "");

        /// <summary>
        ///     Sets the value of this property to <paramref name="value" /> and raises a
        ///     <see cref="INotifyPropertyChanged.PropertyChanged" /> event if the new value was different from the old value.
        /// </summary>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns></returns>
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
        ///     Intended to only be used in property <c>set</c> accessors.
        /// </remarks>
        IDependencyExpression Set(int value, [CallerMemberName] string propertyName = "");

        /// <summary>
        ///     Sets the value of this property to <paramref name="value" /> and raises a
        ///     <see cref="INotifyPropertyChanged.PropertyChanged" /> event if the new value was different from the old value.
        /// </summary>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns></returns>
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
        ///     Intended to only be used in property <c>set</c> accessors.
        /// </remarks>
        IDependencyExpression Set(uint value, [CallerMemberName] string propertyName = "");

        /// <summary>
        ///     Sets the value of this property to <paramref name="value" /> and raises a
        ///     <see cref="INotifyPropertyChanged.PropertyChanged" /> event if the new value was different from the old value.
        /// </summary>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns></returns>
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
        ///     Intended to only be used in property <c>set</c> accessors.
        /// </remarks>
        IDependencyExpression Set(long value, [CallerMemberName] string propertyName = "");

        /// <summary>
        ///     Sets the value of this property to <paramref name="value" /> and raises a
        ///     <see cref="INotifyPropertyChanged.PropertyChanged" /> event if the new value was different from the old value.
        /// </summary>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns></returns>
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
        ///     Intended to only be used in property <c>set</c> accessors.
        /// </remarks>
        IDependencyExpression Set(ulong value, [CallerMemberName] string propertyName = "");

        /// <summary>
        ///     Sets the value of this property to <paramref name="value" /> and raises a
        ///     <see cref="INotifyPropertyChanged.PropertyChanged" /> event if the new value was different from the old value.
        /// </summary>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns></returns>
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
        ///     Intended to only be used in property <c>set</c> accessors.
        /// </remarks>
        IDependencyExpression Set(string value, [CallerMemberName] string propertyName = "");

        /// <summary>
        ///     Sets the value of this property to <paramref name="value" /> and raises a
        ///     <see cref="INotifyPropertyChanged.PropertyChanged" /> event if the new value was different from the old value.
        /// </summary>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="propertyName">
        ///     The name of the property. Filled in by the compiler automatically, do not specify
        ///     explicitly.
        /// </param>
        /// <returns></returns>
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
        ///     Intended to only be used in property <c>set</c> accessors.
        /// </remarks>
        IDependencyExpression Set(DateTime value, [CallerMemberName] string propertyName = "");
    }
}