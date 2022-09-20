﻿using FullSearch.Services.Impl;
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

            var documentsSet = DocumentExtractor.DocumentsSet().Take(10000).ToArray();
            //new SimpleSearcher().Search("Monday", documentsSet);
            new SimpleSearcherV2().SearchV1("Monday", documentsSet);
        }
    }
}
