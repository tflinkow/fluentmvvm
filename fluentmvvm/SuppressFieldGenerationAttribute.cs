using System;

using JetBrains.Annotations;

namespace FluentMvvm
{
    /// <inheritdoc />
    /// <summary>Indicates that no backing field shall be generated for the property or type the attribute is applied to.</summary>
    /// <remarks>If applied to a type, no backing field is generated for any property of that type.</remarks>
    /// <seealso cref="Attribute" />
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public sealed class SuppressFieldGenerationAttribute : Attribute
    {
    }
}