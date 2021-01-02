using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using fluentmvvm.benchmarks.net31.Utils;

namespace fluentmvvm.benchmarks.net31.Comparisons
{
    [SimpleJob(RunStrategy.Throughput, RuntimeMoniker.NetCoreApp31)]
    public class GetStringComparison
    {
        private readonly DefaultViewModel defaultViewModel = new DefaultViewModel();
        private readonly ExpressionViewModel expressionViewModel = new ExpressionViewModel();
        private readonly FluentViewModel fluentViewModel = new FluentViewModel();

        [Benchmark(Baseline = true)]
        public string GetOnly_Default()
        {
            return this.defaultViewModel.SetOnly;
        }

        [Benchmark]
        public string GetOnly_Expression()
        {
            return this.expressionViewModel.SetOnly;
        }

        [Benchmark]
        public string GetOnly_Fluent()
        {
            return this.fluentViewModel.SetOnly;
        }
    }
}