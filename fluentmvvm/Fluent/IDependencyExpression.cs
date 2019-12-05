using System.ComponentModel;
using System.Windows.Input;

using JetBrains.Annotations;

using Microsoft.CSharp.RuntimeBinder;

namespace FluentMvvm.Fluent
{
    /// <summary>Provides method to express dependencies between properties or between a property and a command.</summary>
    [PublicAPI]
    public interface IDependencyExpression
    {
        /// <summary>
        ///     Raises a <see cref="INotifyPropertyChanged.PropertyChanged" /> event for the property
        ///     <paramref name="propertyName" /> if the new property value was different from the old value.
        /// </summary>
        /// <param name="propertyName">The name of the property that changes when this property changes.</param>
        [NotNull]
        IDependencyExpression Affects([CanBeNull] string propertyName);

        /// <summary>
        ///     Raises a <see cref="ICommand.CanExecuteChanged" /> event for the <see cref="ICommand" /> if the new property
        ///     value was different from the old value.
        /// </summary>
        /// <remarks>
        ///     For this method to work correctly, <paramref name="command" /> must provide a
        ///     <c>public void RaiseCanExecuteChanged()</c> method.
        /// </remarks>
        /// <param name="command">The command.</param>
        /// <exception cref="RuntimeBinderException"><paramref name="command"/> does not provide a public, parameterless <c>RaiseCanExecuteChanged</c> method.</exception>
        [NotNull]
        IDependencyExpression Affects([CanBeNull] ICommand command);

        /// <summary>Returns a value indicating whether the new property value was different from the old value.</summary>
        /// <returns><c>true</c> if the new property value was different from the old value; otherwise, <c>false</c>.</returns>
        bool WasUpdated();
    }
}