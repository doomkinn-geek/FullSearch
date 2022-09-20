using FullSearch.Services;
using FullSearch.Services.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FullSearch
{
    internal class Sample1
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services => {
                    services.AddDbContext<DocumentDbContext>(options =>
                    {
                        options.UseSqlServer(@"data source=DESKTOP-FANBABQ\SQLEXPRESS;initial catalog=DocumentsDB;User Id=DocumentsDB;Password=123456;MultipleActiveResultSets=True;App=EntityFramework");
                    });
                    services.AddTransient<IDocumentRepository, DocumentRepository>();
                })
                .Build();
            host.Services.GetRequiredService<IDocumentRepository>().LoadDocuments();
        }
    }
}