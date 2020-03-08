using System;
using System.Windows.Input;

namespace FluentMvvm.Tests.TestData
{
    public class TestICommandNoRaiseMethod : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
        }

        protected void RaiseEvent()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }
}