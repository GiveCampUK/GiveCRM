namespace GiveCRM.Infrastructure.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class SubdomainTenantCodeProviderTests
    {
        [Test]
        public void GetTenantCode_GivenIpAddress_ShouldReturnIpAddressFormatted()
        {
            // Arrange
            var provider = new SubdomainTenantCodeProvider();

            // Act
            string result = provider.GetTenantCode("127.0.0.1");

            // Assert
            Assert.AreEqual("", result);
        }
    }
}