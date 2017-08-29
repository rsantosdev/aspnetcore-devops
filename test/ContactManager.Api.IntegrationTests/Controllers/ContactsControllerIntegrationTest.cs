using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ContactManager.Api.IntegrationTests.Fixtures;
using ContactManager.Api.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace ContactManager.Api.IntegrationTests.Controllers
{
    public class ContactsControllerIntegrationTest : BaseIntegrationTestController
    {
        public ContactsControllerIntegrationTest(ContactManagerApiFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task CanGetContactsEmpty()
        {
            var response = await Client.GetAsync("/api/contacts");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<List<Contact>>();
            Assert.Empty(result);
        }

        [Fact]
        public async Task CanGetContacts()
        {
            var contact = new Contact
            {
                Name = "John Snow",
                Email = "snow@housestark.com"
            };
            DataContext.Contacts.Add(contact);
            await DataContext.SaveChangesAsync();
            
            var response = await Client.GetAsync("/api/contacts");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<List<Contact>>();
            Assert.NotEmpty(result);
            Assert.Equal(contact.Name, result[0].Name);
        }
    }
}