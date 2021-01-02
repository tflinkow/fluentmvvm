using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using fluentmvvm.benchmarks.net31.Utils;

namespace fluentmvvm.benchmarks.net31.Comparisons
{
    [SimpleJob(RunStrategy.Throughput, RuntimeMoniker.NetCoreApp31)]
    public class SetStructComparison
    {
        private readonly DefaultViewModel defaultViewModel = new DefaultViewModel();
        private readonly ExpressionViewModel expressionViewModel = new ExpressionViewModel();
        private readonly FluentViewModel fluentViewModel = new FluentViewModel();

        [Benchmark(Baseline = true)]
        public void SetStruct_Default()
        {
            this.defaultViewModel.Struct = new SampleStruct();
        }

        [Benchmark]
        public void SetStruct_Expression()
        {
            this.expressionViewModel.Struct = new SampleStruct();
        }

        [Benchmark]
        public void SetStruct_Fluent()
        {
            this.fluentViewModel.Struct = new SampleStruct();
        }
    }
}