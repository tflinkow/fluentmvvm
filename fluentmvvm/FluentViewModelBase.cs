using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using FluentMvvm.Fluent;

using JetBrains.Annotations;

namespace FluentMvvm
{
    /// <summary>
    ///     A base class for ViewModels implementing <see cref="INotifyPropertyChanged" /> and providing a fluent API for
    ///     property setters.
    /// </summary>
    /// <seealso cref="FluentMvvm.Fluent.IPropertySetExpression" />
    /// <seealso cref="FluentMvvm.Fluent.IConditionalExpression" />
    /// <seealso cref="FluentMvvm.Fluent.IDependencyExpression" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <inheritdoc cref="IPropertySetExpression" />
    /// <inheritdoc cref="IConditionalExpression" />
    [PublicAPI]
    [NoReorder]
    public abstract class FluentViewModelBase : IPropertySetExpression, IConditionalExpression, IDependencyExpression, INotifyPropertyChanged
    {
        /// <summary>The dynamically generated type containing the backing fields for the concrete view model.</summary>
        [CanBeNull]
        private readonly IBackingFieldProvider backingFieldProvider;

        private bool hasBackingFieldProvider;

        /// <summary>Initializes a new instance of the <see cref="FluentViewModelBase" /> class.</summary>
        protected FluentViewModelBase()
        {
            this.backingFieldProvider = BackingFieldProvider.Get(this.GetType());
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

        /// <summary>Gets the value of the specified property.</summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The value of the property.</returns>
        /// <exception cref="ArgumentException">
        ///     no public writable instance property named <paramref name="propertyName" /> could
        ///     be found.
        /// </exception>
        /// <exception cref="NullReferenceException">
        ///     the type has no public writable instance methods at all -or- the type is
        ///     marked with <see cref="SuppressFieldGenerationAttribute" />.
        /// </exception>
        [CanBeNull]
        public T Get<T>([CallerMemberName] [CanBeNull] string propertyName = null)
        {
            return (T) this.backingFieldProvider.GetValueOf(propertyName);
        }

        /// <inheritdoc />
        public IDependencyExpression Set([CanBeNull] object value, [CallerMemberName] string propertyName = null)
        {
            if (this.backingFieldProvider.SetValueOf(propertyName, value))
            {
                this.RaisePropertyChanged(propertyName);
                return this;
            }

            return EmptyFluentAction.Default;
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
        [ContractAnnotation("=> true")]
        bool IDependencyExpression.WasUpdated()
        {
            return true;
        }

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Notifies listeners that the property <paramref name="propertyName" /> has changed.</summary>
        /// <param name="propertyName">
        ///     The name of the property that has changed. You should not provide the name yourself and rely
        ///     on the compiler inserting the name automatically for you.
        /// </param>
        protected void RaisePropertyChanged([CallerMemberName] [NotNull] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}