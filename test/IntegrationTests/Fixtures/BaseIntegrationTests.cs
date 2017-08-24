using System.Net.Http;
using api.Models;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IntegrationTests.Fixtures
{
    [Collection("Api collection")]
    public class BaseIntegrationTests
    {
        protected readonly TestServer Server;
        protected readonly HttpClient Client;
        protected readonly ApiDbContext DataContext;

        
        public BaseIntegrationTests(ApiFixture fixture)
        {
            Server = fixture.Server;
            Client = fixture.Client;
            DataContext = fixture.DataContext;

            ClearDb();
        }

        private void ClearDb()
        {
            var commands = new[]
            {
                "EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'",
                "EXEC sp_MSForEachTable 'DELETE FROM ?'",
                "EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'"
            };

            DataContext.Database.OpenConnection();
        }
    }
}