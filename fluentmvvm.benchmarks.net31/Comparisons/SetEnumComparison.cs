using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using fluentmvvm.benchmarks.net31.Utils;

namespace fluentmvvm.benchmarks.net31.Comparisons
{
    [SimpleJob(RunStrategy.Throughput, RuntimeMoniker.NetCoreApp31)]
    public class SetEnumComparison
    {
        private readonly DefaultViewModel defaultViewModel = new DefaultViewModel();
        private readonly ExpressionViewModel expressionViewModel = new ExpressionViewModel();
        private readonly FluentViewModel fluentViewModel = new FluentViewModel();

        [Benchmark(Baseline = true)]
        public void SetEnum_Default()
        {
            this.defaultViewModel.Enum = SampleEnum.Blue;
        }

        [Benchmark]
        public void SetEnum_Expression()
        {
            this.expressionViewModel.Enum = SampleEnum.Blue;
        }

        [Benchmark]
        public void SetEnum_Fluent()
        {
            this.fluentViewModel.Enum = SampleEnum.Blue;
        }
    }
}