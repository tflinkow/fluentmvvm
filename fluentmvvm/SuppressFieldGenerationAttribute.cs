using System;

namespace FluentMvvm
{
    /// <inheritdoc />
    /// <summary>Indicates that no backing field shall be generated for the property or type the attribute is applied to.</summary>
    /// <remarks>If applied to a type, no backing field is generated for any property of that type.</remarks>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public sealed class SuppressFieldGenerationAttribute : Attribute
    {
    }
}