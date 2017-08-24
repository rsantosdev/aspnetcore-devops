using System;
using Xunit;

namespace IntegrationTests
{
    public class SampleTest
    {
        [Fact]
        public void Test1()
        {
            Assert.NotEqual(1, 2);
        }
    }
}
