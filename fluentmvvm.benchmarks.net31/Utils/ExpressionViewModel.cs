using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluentMvvm;

namespace fluentmvvm.benchmarks.net31.Utils
{
    public sealed class ExpressionViewModel
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

        public SampleStruct Struct
        {
            get => this.sampleStruct;
            set => this.Set(() => this.sampleStruct = value, this.sampleStruct, value);
        }

        public SampleClass Class
        {
            get => this.sampleClass;
            set => this.Set(() => this.sampleClass = value, this.sampleClass, value);
        }

        public SampleEnum Enum
        {
            get => this.sampleEnum;
            set => this.Set(() => this.sampleEnum = value, this.sampleEnum, value);
        }

        public int Integer
        {
            get => this.integer;
            set => this.Set(() => this.integer = value, this.integer, value);
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