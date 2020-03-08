using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FluentMvvm.Benchmarks
{
    public sealed class DefaultViewModel : IBenchmarkViewModel
    {
        private string setOnly;
        private string setAndNotifyOtherProperty;
        private string setAndNotifyCommand;
        private SampleStruct sampleStruct;
        private SampleClass sampleClass;
        private SampleEnum sampleEnum;
        private int integer;

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
            set
            {
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
            set
            {
                if (this.setAndNotifyCommand != value)
                {
                    this.setAndNotifyCommand = value;
                    this.RaisePropertyChanged();
                    this.MyCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public SampleStruct Struct
        {
            get => this.sampleStruct;
            set
            {
                if (!this.sampleStruct.Equals(value))
                {
                    this.sampleStruct = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public SampleClass Class
        {
            get => this.sampleClass;
            set 
            {
                if (this.sampleClass != value)
                {
                    this.sampleClass = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public SampleEnum Enum
        {
            get => this.sampleEnum;
            set
            {
                if (this.sampleEnum != value)
                {
                    this.sampleEnum = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public int Integer
        {
            get => this.integer;
            set
            {
                if (this.integer != value)
                {
                    this.integer = value;
                    this.RaisePropertyChanged();
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