namespace FluentMvvm.Tests.Models
{
    internal sealed class TestViewModel : FluentViewModelBase
    {
        public int Id
        {
            get => this.Get<int>();
            set => this.Set(value);
        }

        public string Name
        {
            get => this.Get<string>();
            set => this.Set(value);
        }
    }
}