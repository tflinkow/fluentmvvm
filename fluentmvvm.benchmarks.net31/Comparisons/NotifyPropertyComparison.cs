using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using fluentmvvm.benchmarks.net31.Utils;

namespace fluentmvvm.benchmarks.net31.Comparisons
{
    [SimpleJob(RunStrategy.Throughput, RuntimeMoniker.NetCoreApp31)]
    public class NotifyPropertyComparison
    {
        private readonly DefaultViewModel defaultViewModel = new DefaultViewModel();
        private readonly ExpressionViewModel expressionViewModel = new ExpressionViewModel();
        private readonly FluentViewModel fluentViewModel = new FluentViewModel();

        [Benchmark(Baseline = true)]
        public void NotifyProperty_Default()
        {
            this.defaultViewModel.SetAndNotifyOtherProperty = "42";
        }

        [Benchmark]
        public void NotifyProperty_Expression()
        {
            this.expressionViewModel.SetAndNotifyOtherProperty = "42";
        }

        [Benchmark]
        public void NotifyProperty_Fluent()
        {
            this.fluentViewModel.SetAndNotifyOtherProperty = "42";
        }
    }
}