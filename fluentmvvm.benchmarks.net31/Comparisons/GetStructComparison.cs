using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using fluentmvvm.benchmarks.net31.Utils;

namespace fluentmvvm.benchmarks.net31.Comparisons
{
    [SimpleJob(RunStrategy.Throughput, RuntimeMoniker.NetCoreApp31)]
    public class GetStructComparison
    {
        private readonly DefaultViewModel defaultViewModel = new DefaultViewModel();
        private readonly ExpressionViewModel expressionViewModel = new ExpressionViewModel();
        private readonly FluentViewModel fluentViewModel = new FluentViewModel();

        [Benchmark(Baseline = true)]
        public SampleStruct GetStruct_Default()
        {
            return this.defaultViewModel.Struct;
        }

        [Benchmark]
        public SampleStruct GetStruct_Expression()
        {
            return this.expressionViewModel.Struct;
        }

        [Benchmark]
        public SampleStruct GetStruct_Fluent()
        {
            return this.fluentViewModel.Struct;
        }
    }
}