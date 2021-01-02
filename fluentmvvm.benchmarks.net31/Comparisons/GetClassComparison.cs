using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using fluentmvvm.benchmarks.net31.Utils;

namespace fluentmvvm.benchmarks.net31.Comparisons
{
    [SimpleJob(RunStrategy.Throughput, RuntimeMoniker.NetCoreApp31)]
    public class GetClassComparison
    {
        private readonly DefaultViewModel defaultViewModel = new DefaultViewModel();
        private readonly ExpressionViewModel expressionViewModel = new ExpressionViewModel();
        private readonly FluentViewModel fluentViewModel = new FluentViewModel();

        [Benchmark(Baseline = true)]
        public SampleClass GetClass_Default()
        {
            return this.defaultViewModel.Class;
        }

        [Benchmark]
        public SampleClass GetClass_Expression()
        {
            return this.expressionViewModel.Class;
        }

        [Benchmark]
        public SampleClass GetClass_Fluent()
        {
            return this.fluentViewModel.Class;
        }
    }
}