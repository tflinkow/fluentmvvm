using System.ComponentModel;
using System.Runtime.CompilerServices;

using JetBrains.Annotations;

namespace FluentMvvm.Fluent
{
    /// <summary>Provides methods that can be used to set a property value.</summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [PublicAPI]
    public interface IPropertySetExpression
    {
        /// <summary>
        ///     Sets the value of this property to <paramref name="value" /> and raises a
        ///     <see cref="INotifyPropertyChanged.PropertyChanged" /> event if the new value was different from the old value.
        /// </summary>
        /// <remarks>Intended to be used in property <c>set</c> accessors only.</remarks>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="propertyName">The name of the property. Filled in by the compiler automatically.</param>
        [NotNull]
        IDependencyExpression Set([CanBeNull] object value, [CallerMemberName] [NotNull] string propertyName = null);
    }
}