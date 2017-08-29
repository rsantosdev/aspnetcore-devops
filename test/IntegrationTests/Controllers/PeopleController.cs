
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;
using IntegrationTests.Fixtures;
using Xunit;
using System.Net.Http;
using System;

namespace IntegrationTests.Controllers
{
    public class PeopleControllerIntegrationTests : BaseIntegrationTests
    {
        private const string BaseUrl = "/api/people";
        
        public PeopleControllerIntegrationTests(ApiFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task ShouldReturnEmptyListWhenNoData()
        {
            var response = await Client.GetAsync(BaseUrl);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsAsync<List<Person>>();
            Assert.Empty(data);
        }
    }
}