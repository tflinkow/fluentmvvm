using FluentMvvm;

namespace fluentmvvm.benchmarks.net31.Utils
{
    public sealed class FluentViewModel : FluentViewModelBase
    {
        public FluentViewModel() : base(true){}

        public string SetOnly
        {
            get => this.GetString();
            set => this.Set(value);
        }

        public string SetAndNotifyOtherProperty
        {
            get => this.GetString();
            set => this.Set(value).Affects(nameof(this.SetOnly));
        }

        public string SetAndNotifyCommand
        {
            get => this.GetString();
            set => this.Set(value).Affects(this.MyCommand);
        }
        //[SuppressFieldGeneration]
        public SampleStruct Struct
        {
            get => this.Get<SampleStruct>();
            set => this.Set(value);
        }

        //[SuppressFieldGeneration]
        public SampleClass Class
        {
            get => this.Get<SampleClass>();
            set => this.Set(value);
        }

        //[SuppressFieldGeneration]
        public SampleEnum Enum
        {
            get => this.Get<SampleEnum>();
            set => this.Set(value);
        }

        public int Integer
        {
            get => this.GetInt32();
            set => this.Set(value);
        }

        public IWpfCommand MyCommand { get; } = new Command();

        private int intRef;

        public int IntegerRef
        {
            get => this.intRef;
            set => this.Set(value, ref this.intRef);
        }
    }
}