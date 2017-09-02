using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ContactManager.Api.IntegrationTests.Fixtures;
using Xunit;

namespace ContactManager.Api.IntegrationTests.Controllers
{
    public class ValuesControllerIntegrationTest : BaseIntegrationTestController
    {
        public ValuesControllerIntegrationTest(ContactManagerApiFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task CanGetValues()
        {
            var response = await Client.GetAsync("/api/values");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<List<string>>();
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count);
        }
    }
}