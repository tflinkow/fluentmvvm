using System.ComponentModel;

namespace FluentMvvm.Benchmarks
{
    public interface IBenchmarkViewModel : INotifyPropertyChanged
    {
        string SetOnly { get; set; }
        string SetAndNotifyOtherProperty { set; }
        string SetAndNotifyCommand { set; }

        IWpfCommand MyCommand { get; }
    }
}