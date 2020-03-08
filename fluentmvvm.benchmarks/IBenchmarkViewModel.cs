using System.ComponentModel;

namespace FluentMvvm.Benchmarks
{
    public interface IBenchmarkViewModel : INotifyPropertyChanged
    {
        string SetOnly { get; set; }
        string SetAndNotifyOtherProperty { set; }
        string SetAndNotifyCommand { set; }

        SampleStruct Struct { get; set; }
        SampleClass Class { get; set; }
        SampleEnum Enum { get; set; }
        int Integer { get; set; }

        IWpfCommand MyCommand { get; }
    }
}