namespace FluentMvvm.Benchmarks
{
    public sealed class FluentViewModel : FluentViewModelBase, IBenchmarkViewModel
    {
        public string SetOnly
        {
            get => this.Get<string>();
            set => this.Set(value);
        }

        public string SetAndNotifyOtherProperty
        {
            get => this.Get<string>();
            set => this.Set(value).Affects(nameof(this.SetOnly));
        }

        public string SetAndNotifyCommand
        {
            get => this.Get<string>();
            set => this.Set(value).Affects(this.MyCommand);
        }

        public IWpfCommand MyCommand { get; } = new Command();
    }
}