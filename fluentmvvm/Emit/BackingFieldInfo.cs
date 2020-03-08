using System.ComponentModel;
using System.Reflection;

namespace FluentMvvm.Emit
{
    /// <summary>
    ///     A class containing all relevant information for creating the backing field for a property.
    /// </summary>
    internal sealed class BackingFieldInfo
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BackingFieldInfo" /> class.
        /// </summary>
        /// <param name="property">The property to build a backing field for.</param>
        public BackingFieldInfo(PropertyInfo property)
        {
            this.Property = property;

            DefaultValueAttribute defaultValueAttribute = property.GetCustomAttribute<DefaultValueAttribute>();

            if (defaultValueAttribute != null)
            {
                this.HasOverriddenDefaultValue = true;
                this.DefaultValue = defaultValueAttribute.Value;
            }
        }

        /// <summary>
        ///     Gets the property to build a backing field for.
        /// </summary>
        /// <value>
        ///     The property to build a backing field for.
        /// </value>
        internal PropertyInfo Property { get; }

        /// <summary>
        ///     Gets or sets the generated backing field.
        /// </summary>
        /// <value>
        ///     The generated backing field.
        /// </value>
        internal FieldInfo? Field { get; set; }

        /// <summary>
        ///     Gets a value indicating whether the property has overridden default value, that is, whether a
        ///     <see cref="DefaultValueAttribute" /> is applied to it.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has overridden default value; otherwise, <c>false</c>.
        /// </value>
        internal bool HasOverriddenDefaultValue { get; }

        /// <summary>
        ///     Gets the default value.
        /// </summary>
        /// <value>
        ///     The default value.
        /// </value>
        internal object? DefaultValue { get; }
    }
}