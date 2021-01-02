using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using fluentmvvm.benchmarks.net31.Utils;

namespace fluentmvvm.benchmarks.net31.Comparisons
{
    [SimpleJob(RunStrategy.Throughput, RuntimeMoniker.NetCoreApp31)]
    public class GetEnumComparison
    {
        private readonly DefaultViewModel defaultViewModel = new DefaultViewModel();
        private readonly ExpressionViewModel expressionViewModel = new ExpressionViewModel();
        private readonly FluentViewModel fluentViewModel = new FluentViewModel();

        [Benchmark(Baseline = true)]
        public SampleEnum GetEnum_Default()
        {
            return this.defaultViewModel.Enum;
        }

        [Benchmark]
        public SampleEnum GetEnum_Expression()
        {
            return this.expressionViewModel.Enum;
        }

        [Benchmark]
        public SampleEnum GetEnum_Fluent()
        {
            return this.fluentViewModel.Enum;
        }
    }
}