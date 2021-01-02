using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using fluentmvvm.benchmarks.net31.Utils;

namespace fluentmvvm.benchmarks.net31.Comparisons
{
    [SimpleJob(RunStrategy.Throughput, RuntimeMoniker.NetCoreApp31)]
    public class SetStringComparison
    {
        private readonly DefaultViewModel defaultViewModel = new DefaultViewModel();
        private readonly ExpressionViewModel expressionViewModel = new ExpressionViewModel();
        private readonly FluentViewModel fluentViewModel = new FluentViewModel();

        [Benchmark(Baseline = true)]
        public void SetString_Default()
        {
            this.defaultViewModel.SetOnly = "42";
        }

        [Benchmark]
        public void SetString_Expression()
        {
            this.expressionViewModel.SetOnly = "42";
        }

        [Benchmark]
        public void SetString_Fluent()
        {
            this.fluentViewModel.SetOnly = "42";
        }
    }
}