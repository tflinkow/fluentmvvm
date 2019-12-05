using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using JetBrains.Annotations;

namespace FluentMvvm.Fluent
{
    /// <summary>Implements methods for use in fluent call chains.</summary>
    /// <seealso cref="IPropertySetExpression" />
    /// <seealso cref="IDependencyExpression" />
    internal sealed class FluentAction : IPropertySetExpression, IDependencyExpression
    {
        /// <summary>The dynamically generated type containing the backing fields for the concrete view model.</summary>
        [CanBeNull]
        private readonly IBackingFieldProvider backingFieldProvider;

        /// <summary>The method raising the <see cref="INotifyPropertyChanged.PropertyChanged"/> event.</summary>
        [NotNull]
        private readonly Action<string> raisePropertyChanged;

        /// <summary>Initializes a new instance of the <see cref="FluentAction" /> class.</summary>
        /// <param name="backingFieldProvider">The backing field provider.</param>
        /// <param name="raisePropertyChanged">The method raising the <see cref="INotifyPropertyChanged.PropertyChanged"/> event.</param>
        public FluentAction([CanBeNull] IBackingFieldProvider backingFieldProvider, [NotNull] Action<string> raisePropertyChanged)
        {
            this.backingFieldProvider = backingFieldProvider;
            this.raisePropertyChanged = raisePropertyChanged;
        }

        /// <inheritdoc />
        public IDependencyExpression Affects(string propertyName)
        {
            this.raisePropertyChanged(propertyName);

            return this;
        }

        /// <inheritdoc />
        public IDependencyExpression Affects(ICommand command)
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
        public bool WasUpdated()
        {
            return true;
        }

        /// <inheritdoc />
        public IDependencyExpression Set(object value, [CallerMemberName] string propertyName = null)
        {
            Debug.Assert(!String.IsNullOrWhiteSpace(propertyName), $"{nameof(propertyName)} may not be null, empty or white-space.");
            Debug.Assert(this.backingFieldProvider != null, $"{this.backingFieldProvider} should only be null if the view model does not contain public writable instance properties. If that is the case, {nameof(this.Set)} should not be called.");

            if (this.backingFieldProvider.SetValueOf(propertyName, value))
            {
                this.raisePropertyChanged(propertyName);
                return this;
            }

            return EmptyFluentAction.Default;
        }
    }
}