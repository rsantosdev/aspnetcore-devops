using System.Threading.Tasks;
using IntegrationTests.Fixtures;
using Xunit;

namespace IntegrationTests
{
    [Collection("Api collection")]
    public class HelloWorldIntegrationTest
    {
        ApiFixture fixture;

        public HelloWorldIntegrationTest(ApiFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task ShouldReturnHelloWorld()
        {
            var response = await fixture.Client.GetAsync("/");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsStringAsync();
            Assert.Equal("Hello World!", data);
        }
    }
}
