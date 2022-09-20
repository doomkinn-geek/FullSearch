using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using FullSearch.Services.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSearch
{
    internal class Sample2
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services => {
                    services.AddDbContext<DocumentDbContext>(options =>
                    {
                        options.UseSqlServer(@"data source=DESKTOP-FANBABQ\SQLEXPRESS;initial catalog=DocumentsDB;User Id=DocumentsDB;Password=123456;MultipleActiveResultSets=True;App=EntityFramework");
                    });                    
                })
                .Build();

            //var documentsSet = DocumentExtractor.DocumentsSet().Take(10000).ToArray();
            //new SimpleSearcher().Search("Monday", documentsSet);
            //new SimpleSearcherV2().SearchV1("Monday", documentsSet);
            //new SimpleSearcherV3().SearchV3("Monday", documentsSet);

            BenchmarkSwitcher.FromAssembly(typeof(Sample2).Assembly).Run(args, new BenchmarkDotNet.Configs.DebugInProcessConfig());
            BenchmarkRunner.Run<SearchBenchmarkV1>();
        }        
    }

    [MemoryDiagnoser]
    [WarmupCount(1)]
    [IterationCount(5)]
    public class SearchBenchmarkV1
    {
        private readonly string[] _documentsSet;

        public SearchBenchmarkV1()
        {
            _documentsSet = DocumentExtractor.DocumentsSet().Take(10000).ToArray();
        }

        [Benchmark]
        public void SimpleSearch()
        {
            new SimpleSearcherV3().SearchV3Enumerable("Monday", _documentsSet).ToArray();
        }

    }
}
