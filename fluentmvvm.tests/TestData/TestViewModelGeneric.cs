namespace FluentMvvm.Tests.TestData
{
    internal sealed class TestViewModelGeneric<T> : FluentViewModelBase
    {
        public T Value
        {
            get => this.Get<T>();
            set => this.Set(value);
        }
    }
}