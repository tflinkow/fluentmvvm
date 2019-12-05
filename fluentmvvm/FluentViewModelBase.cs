using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using FluentMvvm.Fluent;

using JetBrains.Annotations;

namespace FluentMvvm
{
    /// <inheritdoc cref="IPropertySetExpression" />
    /// <inheritdoc cref="IConditionalExpression"/>
    /// <summary>
    ///     A base class for ViewModels implementing <see cref="INotifyPropertyChanged" /> and providing a fluent API for
    ///     property setters.
    /// </summary>
    [PublicAPI]
    [NoReorder]
    public abstract class FluentViewModelBase : IPropertySetExpression, IConditionalExpression, INotifyPropertyChanged
    {
        /// <summary>An object for use in fluent method calls.</summary>
        [NotNull]
        private readonly FluentAction action;

        /// <summary>The dynamically generated type containing the backing fields for the concrete view model.</summary>
        [CanBeNull]
        private readonly IBackingFieldProvider backingFieldProvider;

        /// <summary>Initializes a new instance of the <see cref="FluentViewModelBase" /> class.</summary>
        protected FluentViewModelBase()
        {
            this.backingFieldProvider = BackingFieldProvider.Get(this.GetType());
            this.action = new FluentAction(this.backingFieldProvider, this.RaisePropertyChanged);
        }

        /// <inheritdoc />
        public IPropertySetExpression When(bool condition)
        {
            return condition ? (IPropertySetExpression) this.action : EmptyFluentAction.Default;
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
        [CanBeNull]
        public T Get<T>([CallerMemberName] [NotNull] string propertyName = null)
        {
            Debug.Assert(!String.IsNullOrWhiteSpace(propertyName), $"{nameof(propertyName)} may not be null, empty or white-space.");
            Debug.Assert(this.backingFieldProvider != null, $"{this.backingFieldProvider} should only be null if the view model does not contain public writable instance properties. If that is the case, {nameof(this.Get)} should not be called.");

            return (T) this.backingFieldProvider.GetValueOf(propertyName);
        }

        /// <inheritdoc />
        public IDependencyExpression Set([CanBeNull] object value, [CallerMemberName] string propertyName = null)
        {
            Debug.Assert(!String.IsNullOrWhiteSpace(propertyName), $"{nameof(propertyName)} may not be null, empty or white-space.");
            Debug.Assert(this.backingFieldProvider != null, $"{this.backingFieldProvider} should only be null if the view model does not contain public writable instance properties. If that is the case, {nameof(this.Set)} should not be called.");

            if (this.backingFieldProvider.SetValueOf(propertyName, value))
            {
                this.RaisePropertyChanged(propertyName);
                return this.action;
            }

            return EmptyFluentAction.Default;
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