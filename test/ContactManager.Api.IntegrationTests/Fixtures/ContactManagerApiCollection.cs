using Xunit;

namespace ContactManager.Api.IntegrationTests.Fixtures
{
    [CollectionDefinition("Api collection")]
    public class ContactManagerApiCollection : ICollectionFixture<ContactManagerApiFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}