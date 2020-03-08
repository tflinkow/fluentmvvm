using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FluentMvvm.Emit;
using FluentMvvm.Fluent;

namespace FluentMvvm
{
    /// <summary>
    ///     A base class for ViewModels implementing <see cref="INotifyPropertyChanged" /> and providing a fluent API for
    ///     property setters.
    /// </summary>
    /// <seealso cref="FluentMvvm.Fluent.IPropertyGetExpression" />
    /// <seealso cref="FluentMvvm.Fluent.IPropertySetExpression" />
    /// <seealso cref="FluentMvvm.Fluent.IConditionalExpression" />
    /// <seealso cref="FluentMvvm.Fluent.IDependencyExpression" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="IPropertyGetExpression" />
    /// <seealso cref="IPropertySetExpression" />
    /// <seealso cref="IConditionalExpression" />
    /// <seealso cref="IDependencyExpression" />
    /// <seealso cref="INotifyPropertyChanged" />
    /// <inheritdoc cref="IPropertyGetExpression" />
    /// <inheritdoc cref="IPropertyGetExpression" />
    /// <inheritdoc cref="IConditionalExpression" />
    /// <inheritdoc cref="IDependencyExpression" />
    public abstract partial class FluentViewModelBase : IPropertyGetExpression, IPropertySetExpression, IConditionalExpression, IDependencyExpression, INotifyPropertyChanged
    {
        /// <summary>The dynamically generated type containing the backing fields for the concrete view model.</summary>
        private readonly IBackingFieldProvider backingFields;

        /// <summary>
        ///     Initializes a new instance of the <see cref="FluentViewModelBase" /> class.
        /// </summary>
        protected FluentViewModelBase()
        {
            this.backingFields = BackingFieldProvider.Get(this.GetType());
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FluentViewModelBase" /> class with a (possibly stubbed) instance of an
        ///     <see cref="IBackingFieldProvider" />.
        /// </summary>
        /// <param name="backingFieldProvider">The backing field provider.</param>
        internal FluentViewModelBase(IBackingFieldProvider backingFieldProvider)
        {
            this.backingFields = backingFieldProvider;
        }

        /// <inheritdoc />
        public IPropertySetExpression When(bool condition)
        {
            return condition ? (IPropertySetExpression) this : EmptyFluentAction.Default;
        }

        /// <inheritdoc />
        public IPropertySetExpression When(Func<bool> condition)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return this.When(condition());
        }

        /// <inheritdoc />
        IDependencyExpression IDependencyExpression.Affects(string propertyName)
        {
            this.RaisePropertyChanged(propertyName);

            return this;
        }

        /// <inheritdoc />
        IDependencyExpression IDependencyExpression.Affects(ICommand command)
        {
            switch (command)
            {
                case null:
                    return this;
                case IWpfCommand wpfCommand:
                    wpfCommand.RaiseCanExecuteChanged();
                    break;
                default:
                    ((dynamic) command).RaiseCanExecuteChanged();
                    break;
            }

            return this;
        }

        /// <inheritdoc />
        bool IDependencyExpression.WasUpdated()
        {
            return true;
        }

        /// <inheritdoc />
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        ///     Provides a way to run custom code immediately after the field was set and the
        ///     <see cref="INotifyPropertyChanged.PropertyChanged" /> event was raised.
        /// </summary>
        /// <remarks>
        ///     This method will only be called if the value passed to <see cref="Set{T}" /> or its overloads is different from the
        ///     current field value.
        /// </remarks>
        protected virtual void AfterSet()
        {
        }

        /// <summary>
        ///     Notifies listeners that the property <paramref name="propertyName" /> has changed.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property that has changed. You should not provide the name yourself and rely
        ///     on the compiler inserting the name automatically for you.
        /// </param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}