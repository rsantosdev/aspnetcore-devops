using System;
using System.Net.Http;
using System.Net.Http.Headers;
using ContactManager.Api.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ContactManager.Api.IntegrationTests.Fixtures
{
    public class ContactManagerApiFixture : IDisposable    
    {
        public readonly TestServer Server;
        public readonly HttpClient Client;
        public readonly DataContext DataContext;
        public readonly IConfigurationRoot Configuration;
        
        public ContactManagerApiFixture()
        {
            Configuration = BuildConfiguration();

            
            var dbContextOptions = ConfigureDataServices();
            DataContext = new DataContext(dbContextOptions.Options);
            
            var builder = WebHost.CreateDefaultBuilder().UseStartup<Startup>();
            Server = new TestServer(builder);
            Client = Server.CreateClient();

            SetupDatabase();
        }

        private IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        private DbContextOptionsBuilder<DataContext> ConfigureDataServices()
        {
            var opts = new DbContextOptionsBuilder<DataContext>();
            opts.UseSqlite(
                Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("ContactManager.Api")
            );

            return opts;
        }

        private void SetupDatabase()
        {
            DataContext.Database.Migrate();
        }

        public void Dispose()
        {
            DataContext?.Dispose();
            Client?.Dispose();
            Server?.Dispose();
        }
    }
}