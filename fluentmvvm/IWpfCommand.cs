using System.ComponentModel;
using System.Windows.Input;

namespace FluentMvvm
{
    /// <summary>
    ///     The base interface for WPF commands allowing consumers to raise the <see cref="ICommand.CanExecuteChanged" />
    ///     event.
    /// </summary>
    /// <seealso cref="ICommand" />
    public interface IWpfCommand : ICommand
    {
        /// <summary>
        ///     Raises the <see cref="INotifyPropertyChanged.PropertyChanged" /> event.
        /// </summary>
        void RaiseCanExecuteChanged();
    }
}