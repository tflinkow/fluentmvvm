using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using fluentmvvm.benchmarks.net31.Utils;

namespace fluentmvvm.benchmarks.net31.Comparisons
{
    [SimpleJob(RunStrategy.Throughput, RuntimeMoniker.NetCoreApp31)]
    public class GetSingleComparison
    {
        private readonly DefaultViewModel defaultViewModel = new DefaultViewModel();
        private readonly ExpressionViewModel expressionViewModel = new ExpressionViewModel();
        private readonly FluentViewModel fluentViewModel = new FluentViewModel();

        [Benchmark(Baseline = true)]
        public int GetInt_Default()
        {
            return this.defaultViewModel.Integer;
        }

        [Benchmark]
        public int GetInt_Expression()
        {
            return this.expressionViewModel.Integer;
        }

        [Benchmark]
        public int GetInt_Fluent()
        {
            return this.fluentViewModel.Integer;
        }
    }
}