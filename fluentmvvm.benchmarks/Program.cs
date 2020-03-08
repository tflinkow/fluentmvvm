using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace FluentMvvm.Benchmarks
{
    [RyuJitX64Job]
    public class Program
    {
        private readonly IBenchmarkViewModel defaultViewModel = new DefaultViewModel();
        private readonly IBenchmarkViewModel expressionViewModel = new ExpressionViewModel();
        private readonly IBenchmarkViewModel fluentViewModel = new FluentViewModel();

        //private ulong counter = 0;

        [Benchmark]
        public void GetOnly_Default()
        {
            string x = this.defaultViewModel.SetOnly;
        }

        //[Benchmark]
        //public void GetStruct_Default()
        //{
        //    SampleStruct x = this.defaultViewModel.Struct;
        //}

        //[Benchmark]
        //public void GetClass_Default()
        //{
        //    SampleClass x = this.defaultViewModel.Class;
        //}

        //[Benchmark]
        //public void GetEnum_Default()
        //{
        //    SampleEnum x = this.defaultViewModel.Enum;
        //}

        //[Benchmark]
        //public void GetInt_Default()
        //{
        //    int x = this.defaultViewModel.Integer;
        //}

        [Benchmark]
        public void SetOnly_Default()
        {
            this.defaultViewModel.SetOnly = /*this.counter++ % 2 == 0 ? "Hello" :*/ "Hi";
        }

        //[Benchmark]
        //public void SetStruct_Default()
        //{
        //    this.defaultViewModel.Struct = new SampleStruct();
        //}

        //[Benchmark]
        //public void SetClass_Default()
        //{
        //    this.defaultViewModel.Class = new SampleClass();
        //}

        //[Benchmark]
        //public void SetEnum_Default()
        //{
        //    this.defaultViewModel.Enum = SampleEnum.Blue;
        //}

        //[Benchmark]
        //public void SetInt_Default()
        //{
        //    this.defaultViewModel.Integer = 42;
        //}

        [Benchmark]
        public void SetAndNotifyOtherProperty_Default()
        {
            this.defaultViewModel.SetAndNotifyOtherProperty = /*this.counter++ % 2 == 0 ? "Hello" :*/ "Hi";
        }

        [Benchmark]
        public void SetAndNotifyCommand_Default()
        {
            this.defaultViewModel.SetAndNotifyCommand = /*this.counter++ % 2 == 0 ? "Hello" :*/ "Hi";
        }

        [Benchmark]
        public void GetOnly_Expression()
        {
            string x = this.expressionViewModel.SetOnly;
        }

        //[Benchmark]
        //public void GetStruct_Expression()
        //{
        //    SampleStruct x = this.expressionViewModel.Struct;
        //}

        //[Benchmark]
        //public void GetClass_Expression()
        //{
        //    SampleClass x = this.expressionViewModel.Class;
        //}

        //[Benchmark]
        //public void GetEnum_Expression()
        //{
        //    SampleEnum x = this.expressionViewModel.Enum;
        //}

        //[Benchmark]
        //public void GetInt_Expression()
        //{
        //    int x = this.expressionViewModel.Integer;
        //}

        [Benchmark]
        public void SetOnly_Expression()
        {
            this.expressionViewModel.SetOnly = /*this.counter++ % 2 == 0 ? "Hello" :*/ "Hi";
        }

        //[Benchmark]
        //public void SetStruct_Expression()
        //{
        //    this.expressionViewModel.Struct = new SampleStruct();
        //}

        //[Benchmark]
        //public void SetClass_Expression()
        //{
        //    this.expressionViewModel.Class = new SampleClass();
        //}

        //[Benchmark]
        //public void SetEnum_Expression()
        //{
        //    this.expressionViewModel.Enum = SampleEnum.Blue;
        //}

        //[Benchmark]
        //public void SetInt_Expression()
        //{
        //    this.expressionViewModel.Integer = 42;
        //}

        [Benchmark]
        public void SetAndNotifyOtherProperty_Expression()
        {
            this.expressionViewModel.SetAndNotifyOtherProperty = /*this.counter++ % 2 == 0 ? "Hello" :*/ "Hi";
        }

        [Benchmark]
        public void SetAndNotifyCommand_Expression()
        {
            this.expressionViewModel.SetAndNotifyCommand = /*this.counter++ % 2 == 0 ? "Hello" :*/ "Hi";
        }

        [Benchmark]
        public void GetOnly_Fluent()
        {
            string x = this.fluentViewModel.SetOnly;
        }

        //[Benchmark]
        //public void GetStruct_Fluent()
        //{
        //    SampleStruct x = this.fluentViewModel.Struct;
        //}

        //[Benchmark]
        //public void GetClass_Fluent()
        //{
        //    SampleClass x = this.fluentViewModel.Class;
        //}

        //[Benchmark]
        //public void GetEnum_Fluent()
        //{
        //    SampleEnum x = this.fluentViewModel.Enum;
        //}

        //[Benchmark]
        //public void GetInt_Fluent()
        //{
        //    int x = this.fluentViewModel.Integer;
        //}

        [Benchmark]
        public void SetOnly_Fluent()
        {
            this.fluentViewModel.SetOnly = /*this.counter++ % 2 == 0 ? "Hello" :*/ "Hi";
        }

        //[Benchmark]
        //public void SetStruct_Fluent()
        //{
        //    this.fluentViewModel.Struct = new SampleStruct();
        //}

        //[Benchmark]
        //public void SetClass_Fluent()
        //{
        //    this.fluentViewModel.Class = new SampleClass();
        //}

        //[Benchmark]
        //public void SetEnum_Fluent()
        //{
        //    this.fluentViewModel.Enum = SampleEnum.Blue;
        //}

        //[Benchmark]
        //public void SetInt_Fluent()
        //{
        //    this.fluentViewModel.Integer = 42;
        //}

        [Benchmark]
        public void SetAndNotifyOtherProperty_Fluent()
        {
            this.fluentViewModel.SetAndNotifyOtherProperty = /*this.counter++ % 2 == 0 ? "Hello" :*/ "Hi";
        }

        [Benchmark]
        public void SetAndNotifyCommand_Fluent()
        {
            this.fluentViewModel.SetAndNotifyCommand = /*this.counter++ % 2 == 0 ? "Hello" :*/ "Hi";
        }

        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Program>();
        }
    }
}
