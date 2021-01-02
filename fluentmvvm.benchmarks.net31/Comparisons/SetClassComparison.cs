using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using fluentmvvm.benchmarks.net31.Utils;

namespace fluentmvvm.benchmarks.net31.Comparisons
{
    [SimpleJob(RunStrategy.Throughput, RuntimeMoniker.NetCoreApp31)]
    public class SetClassComparison
    {
        private readonly DefaultViewModel defaultViewModel = new DefaultViewModel();
        private readonly ExpressionViewModel expressionViewModel = new ExpressionViewModel();
        private readonly FluentViewModel fluentViewModel = new FluentViewModel();

        [Benchmark(Baseline = true)]
        public void SetClass_Default()
        {
            this.defaultViewModel.Class = new SampleClass();
        }

        [Benchmark]
        public void SetClass_Expression()
        {
            this.expressionViewModel.Class = new SampleClass();
        }

        [Benchmark]
        public void SetClass_Fluent()
        {
            this.fluentViewModel.Class = new SampleClass();
        }
    }
}