using System;

namespace FluentMvvm.Benchmarks
{
    public sealed class Command : IWpfCommand
    {
        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            throw new NotSupportedException();
        }

        public event EventHandler CanExecuteChanged;
    }
}