using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FluentMvvm.Benchmarks
{
    public class DefaultViewModel : IBenchmarkViewModel
    {
        private string setOnly;
        private string setAndNotifyOtherProperty;
        private string setAndNotifyCommand;

        public string SetOnly
        {
            get => this.setOnly;
            set
            {
                if (this.setOnly != value)
                {
                    this.setOnly = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public string SetAndNotifyOtherProperty
        {
            get => this.setAndNotifyOtherProperty;
            set {
                if (this.setAndNotifyOtherProperty != value)
                {
                    this.setAndNotifyOtherProperty = value;
                    this.RaisePropertyChanged();
                    this.RaisePropertyChanged(nameof(this.SetOnly));
                }
            }
        }

        public string SetAndNotifyCommand
        {
            get => this.setAndNotifyCommand;
            set {
                if (this.setAndNotifyCommand != value)
                {
                    this.setAndNotifyCommand = value;
                    this.RaisePropertyChanged();
                    this.MyCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public IWpfCommand MyCommand { get; } = new Command();

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}