using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using fluentmvvm.benchmarks.net31.Utils;

namespace fluentmvvm.benchmarks.net31.Comparisons
{
    [SimpleJob(RunStrategy.Throughput, RuntimeMoniker.NetCoreApp31)]
    public class SetSingleComparison
    {
        private readonly DefaultViewModel defaultViewModel = new DefaultViewModel();
        private readonly ExpressionViewModel expressionViewModel = new ExpressionViewModel();
        private readonly FluentViewModel fluentViewModel = new FluentViewModel();

        [Benchmark(Baseline = true)]
        public void SetInt_Default()
        {
            this.defaultViewModel.Integer = 42;
        }

        [Benchmark]
        public void SetInt_Expression()
        {
            this.expressionViewModel.Integer = 42;
        }

        [Benchmark]
        public void SetInt_Fluent()
        {
            this.fluentViewModel.Integer = 42;
        }

        [Benchmark]
        public void SetInt_FluentRef()
        {
            this.fluentViewModel.IntegerRef = 42;
        }
    }
}