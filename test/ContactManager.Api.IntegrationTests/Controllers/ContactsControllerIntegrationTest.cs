using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ContactManager.Api.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace ContactManager.Api.IntegrationTests.Controllers
{
    public class ContactsControllerIntegrationTest
    {
        [Fact]
        public async Task CanGetContactsEmpty()
        {
            var builder = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>();

            using (var server = new TestServer(builder))
            using (var client = server.CreateClient())
            {
                var response = await client.GetAsync("/api/contacts");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsAsync<List<Contact>>();
                Assert.Empty(result);
            }
        }
    }
}