using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using fluentmvvm.benchmarks.net31.Utils;

namespace fluentmvvm.benchmarks.net31.Comparisons
{
    [SimpleJob(RunStrategy.Throughput, RuntimeMoniker.NetCoreApp31)]
    public class NotifyCommandComparison
    {
        private readonly DefaultViewModel defaultViewModel = new DefaultViewModel();
        private readonly ExpressionViewModel expressionViewModel = new ExpressionViewModel();
        private readonly FluentViewModel fluentViewModel = new FluentViewModel();

        [Benchmark(Baseline = true)]
        public void NotifyCommand_Default()
        {
            this.defaultViewModel.SetAndNotifyCommand = "42";
        }

        [Benchmark]
        public void NotifyCommand_Expression()
        {
            this.expressionViewModel.SetAndNotifyCommand = "42";
        }

        [Benchmark]
        public void NotifyCommand_Fluent()
        {
            this.fluentViewModel.SetAndNotifyCommand = "42";
        }
    }
}