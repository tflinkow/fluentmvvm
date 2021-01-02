using System;
using FluentMvvm;

namespace fluentmvvm.benchmarks.net31.Utils
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