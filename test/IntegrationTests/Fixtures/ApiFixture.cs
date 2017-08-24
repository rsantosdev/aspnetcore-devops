using System;
using System.Net.Http;
using api;
using api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IntegrationTests.Fixtures
{
    public class ApiFixture : IDisposable    
    {
        public readonly TestServer Server;
        public readonly HttpClient Client;
        public readonly ApiDbContext DataContext;
        public readonly IConfigurationRoot Configuration;
        
        public ApiFixture()
        {
            Configuration = BuildConfiguration();

            var opts = new DbContextOptionsBuilder<ApiDbContext>();
            opts.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            DataContext = new ApiDbContext(opts.Options);
            
            var builder = new WebHostBuilder().UseStartup<Startup>();
            Server = new TestServer(builder);
            Client = Server.CreateClient();
        }

        private IConfigurationRoot BuildConfiguration()
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{envName}.json", optional: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        private void SetupDatabase()
        {
            try
            {
                DataContext.Database.EnsureCreated();
                DataContext.Database.Migrate();
            }
            catch (System.Exception)
            {
                //TODO: Add a better logging
                // Does nothing
            }
        }

        public void Dispose()
        {
            DataContext?.Dispose();
            Client?.Dispose();
            Server?.Dispose();
        }
    }
}