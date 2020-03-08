namespace FluentMvvm.Tests.TestData
{
    public class TestICommand : TestICommandNoRaiseMethod
    {
        public void RaiseCanExecuteChanged()
        {
            base.RaiseEvent();
        }
    }
}