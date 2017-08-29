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

            // ClearDb();
        }

        private void ClearDb()
        {
            var commands = new[]
            {
                "DELETE FROM People"
            };

            DataContext.Database.OpenConnection();

            foreach(var cmd in commands)
            {
                DataContext.Database.ExecuteSqlCommand(cmd);
            }

            DataContext.Database.CloseConnection();
        }
    }
}