using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace FluentMvvm.Benchmarks
{
    [RyuJitX64Job]
    public class Program
    {
        private readonly IBenchmarkViewModel defaultViewModel = new DefaultViewModel();
        private readonly IBenchmarkViewModel expressionViewModel = new ExpressionViewModel();
        private readonly IBenchmarkViewModel fluentViewModel = new FluentViewModel();

        [Benchmark]
        public void GetOnly_Default()
        {
            var x = this.defaultViewModel.SetOnly;
        }

        [Benchmark]
        public void SetOnly_Default()
        {
            this.defaultViewModel.SetOnly = "Hello";
        }

        [Benchmark]
        public void SetAndNotifyOtherProperty_Default()
        {
            this.defaultViewModel.SetAndNotifyOtherProperty = "Hello";
        }

        [Benchmark]
        public void SetAndNotifyCommand_Default()
        {
            this.defaultViewModel.SetAndNotifyCommand = "Hello";
        }

        [Benchmark]
        public void GetOnly_Expression()
        {
            var x = this.expressionViewModel.SetOnly;
        }

        [Benchmark]
        public void SetOnly_Expression()
        {
            this.expressionViewModel.SetOnly = "Hello";
        }

        [Benchmark]
        public void SetAndNotifyOtherProperty_Expression()
        {
            this.expressionViewModel.SetAndNotifyOtherProperty = "Hello";
        }

        [Benchmark]
        public void SetAndNotifyCommand_Expression()
        {
            this.expressionViewModel.SetAndNotifyCommand = "Hello";
        }

        [Benchmark]
        public void GetOnly_Fluent()
        {
            var x = this.fluentViewModel.SetOnly;
        }

        [Benchmark]
        public void SetOnly_Fluent()
        {
            this.fluentViewModel.SetOnly = "Hello";
        }

        [Benchmark]
        public void SetAndNotifyOtherProperty_Fluent()
        {
            this.fluentViewModel.SetAndNotifyOtherProperty = "Hello";
        }

        [Benchmark]
        public void SetAndNotifyCommand_Fluent()
        {
            this.fluentViewModel.SetAndNotifyCommand = "Hello";
        }

        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Program>();
        }
    }
}
