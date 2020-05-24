namespace WorkExpression
{
    using System;
    using System.Linq.Expressions;

    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Diagnosers;
    using BenchmarkDotNet.Exporters;
    using BenchmarkDotNet.Jobs;
    using BenchmarkDotNet.Running;

    public static class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run<ExpressionBenchmark>();
        }
    }

    public class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            AddExporter(MarkdownExporter.Default, MarkdownExporter.GitHub);
            AddDiagnoser(MemoryDiagnoser.Default);
            AddJob(Job.LongRun);
            //AddJob(Job.MediumRun);
            //AddJob(Job.ShortRun);
        }
    }

    [Config(typeof(BenchmarkConfig))]
    public class ExpressionBenchmark
    {
        private const int N = 1_000_000;

        private readonly Data data = new Data { Value = 1 };

        private Func<Data, int> byFunction;
        private  Func<Data, int> byExpression; // TODO Faster than func
        // TODO Emit

        [GlobalSetup]
        public void Setup()
        {
            byFunction = CodeFactory.CreateByFunc<Data, int>(x => x.Value);
            byExpression = CodeFactory.CreateByExpression<Data, int>(x => x.Value);
        }

        [Benchmark(OperationsPerInvoke = N)]
        public int ByFunction()
        {
            var ret = 0;
            for (var i = 0; i < N; i++)
            {
                ret = byFunction(data);
            }

            return ret;
        }

        [Benchmark(OperationsPerInvoke = N)]
        public int ByExpression()
        {
            var ret = 0;
            for (var i = 0; i < N; i++)
            {
                ret = byExpression(data);
            }

            return ret;
        }
    }

    public class Data
    {
        public int Value { get; set; }
    }

    public static class CodeFactory
    {
        public static Func<TS, TM> CreateByFunc<TS, TM>(Func<TS, TM> function)
        {
            return function;
        }

        public static Func<TS, TM> CreateByExpression<TS, TM>(Expression<Func<TS, TM>> expression)
        {
            return expression.Compile();
        }

    }
}
