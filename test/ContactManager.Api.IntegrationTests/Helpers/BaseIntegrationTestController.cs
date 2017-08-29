using System.Net.Http;
using ContactManager.Api.Models;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ContactManager.Api.IntegrationTests.Fixtures
{
    [Collection("Api collection")]
    public class BaseIntegrationTestController
    {
        protected readonly TestServer Server;
        protected readonly HttpClient Client;
        protected readonly DataContext DataContext;

        
        public BaseIntegrationTestController(ContactManagerApiFixture fixture)
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
                "DELETE FROM Contacts"
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