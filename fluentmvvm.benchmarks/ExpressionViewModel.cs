using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FluentMvvm.Benchmarks
{
    public sealed class ExpressionViewModel : IBenchmarkViewModel
    {
        private string setOnly;
        private string setAndNotifyOtherProperty;
        private string setAndNotifyCommand;

        public string SetOnly
        {
            get => this.setOnly;
            set => this.Set(() => this.setOnly = value, this.setOnly, value);
        }

        public string SetAndNotifyOtherProperty
        {
            get => this.setAndNotifyOtherProperty;
            set
            {
                this.Set(() => this.setAndNotifyOtherProperty = value, this.setAndNotifyOtherProperty, value);
                this.RaisePropertyChanged(nameof(this.SetOnly));
            }
        }

        public string SetAndNotifyCommand
        {
            get => this.setAndNotifyCommand;
            set
            {
                this.Set(() => this.setAndNotifyCommand = value, this.setAndNotifyCommand, value);
                this.MyCommand.RaiseCanExecuteChanged();
            }
        }

        public IWpfCommand MyCommand { get; } = new Command();

        private void Set<T>(Func<T> assign, T oldValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Object.Equals(oldValue, newValue))
            {
                assign.Invoke();
                this.RaisePropertyChanged(propertyName);
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}