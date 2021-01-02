using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace FluentMvvm.Internals
{
    internal static class TypeHelper
    {
        /// <summary>
        /// Gets the default value for the type of the property, taking into account a <see cref="DefaultValueAttribute"/> if it is applied to the property.
        /// </summary>
        /// <param name="propertyInfo">The property.</param>
        /// <returns>The default value for the type of the property.</returns>
        [return: MaybeNull] // if property.PropertyType is not a value type
        public static object GetDefaultValue(PropertyInfo propertyInfo)
        {
            var defaultValueAttribute = propertyInfo.GetCustomAttribute<DefaultValueAttribute>();

            if (defaultValueAttribute is null)
            {
                return propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null;
            }

            return defaultValueAttribute.Value;
        }

        /// <summary>
        /// Returns a value indicating whether a specific <c>Get</c> or <c>Set</c> overload for <paramref name="type"/> exists.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c>, if <paramref name="type"/> is a reference type that is not <see cref="DateTime"/>, <see cref="Decimal"/> or <see cref="String"/>.</returns>
        [Pure]
        public static bool DoesOverloadExist(Type type)
        {
            if (type.IsPrimitive)
            {
                return true;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.DateTime:
                case TypeCode.Decimal:
                case TypeCode.String:
                    return true;
            }

            return false;
        }
    }
}