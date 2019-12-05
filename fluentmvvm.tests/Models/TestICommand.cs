namespace FluentMvvm.Tests.Models
{
    public class TestICommand : TestICommandNoRaiseMethod
    {
        public void RaiseCanExecuteChanged()
        {
            base.RaiseEvent();
        }
    }
}