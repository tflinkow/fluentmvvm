using BenchmarkDotNet.Running;
using fluentmvvm.benchmarks.net31.Comparisons;

namespace fluentmvvm.benchmarks.net31
{
    public class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run<GetClassComparison>();
            BenchmarkRunner.Run<GetEnumComparison>();
            BenchmarkRunner.Run<GetStructComparison>();
            BenchmarkRunner.Run<GetSingleComparison>();
            BenchmarkRunner.Run<GetStringComparison>();
            BenchmarkRunner.Run<SetClassComparison>();
            BenchmarkRunner.Run<SetEnumComparison>();
            BenchmarkRunner.Run<SetStructComparison>();
            BenchmarkRunner.Run<SetSingleComparison>();
            BenchmarkRunner.Run<SetStringComparison>();
            BenchmarkRunner.Run<NotifyCommandComparison>();
            BenchmarkRunner.Run<NotifyPropertyComparison>();
        }
    }
}
