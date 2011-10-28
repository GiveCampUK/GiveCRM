using System.Collections.Generic;
using GiveCRM.Models;
using GiveCRM.Web.Services.ExcelImport;
using NUnit.Framework;

namespace GiveCRM.Web.Tests.Services.ExcelImport
{
    [TestFixture]
    public class MemberFactory_CreateMember_Should
    {
        private MemberFactory factory;

        [SetUp]
        public void Setup()
        {
            factory = new MemberFactory();
        }

        [Test]
        public void ReturnAMemberWithTheReferencePropertySet_WhenTheInputDataContainsAReferenceProperty()
        {
            const string reference = "SFJ458-AD2";
            var data = new Dictionary<string, object>
                           {
                               {"Reference", reference}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(reference, member.Reference);
        }

        [Test]
        public void ReturnAMemberWithTheTitlePropertySet_WhenTheInputDataContainsATitleProperty()
        {
            const string title = "Mr";
            var data = new Dictionary<string, object>
                           {
                               {"Title", title}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(title, member.Title);
        }

        [Test]
        public void ReturnAMemberWithTheFirstNamePropertySet_WhenTheInputDataContainsAFirstNameProperty()
        {
            const string firstName = "Joe";
            var data = new Dictionary<string, object>
                           {
                               {"FirstName", firstName}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(firstName, member.FirstName);
        }

        [Test]
        public void ReturnAMemberWithTheLastNamePropertySet_WhenTheInputDataContainsALastNameProperty()
        {
            const string lastName = "Bloggs";
            var data = new Dictionary<string, object>
                           {
                               {"LastName", lastName}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(lastName, member.LastName);
        }

        [Test]
        public void ReturnAMemberWithTheSalutationPropertySet_WhenTheInputDataContainsASalutationProperty()
        {
            const string salutation = "Sir";
            var data = new Dictionary<string, object>
                           {
                               {"Salutation", salutation}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(salutation, member.Salutation);
        }

        [Test]
        public void ReturnAMemberWithTheEmailAddressPropertySet_WhenTheInputDataContainsAnEmailAddressProperty()
        {
            const string emailAddress = "joe.bloggs@gmail.com";
            var data = new Dictionary<string, object>
                           {
                               {"EmailAddress", emailAddress}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(emailAddress, member.EmailAddress);
        }

        [Test]
        public void ReturnAMemberWithTheAddressLine1PropertySet_WhenTheInputDataContainsAnAddressLine1Property()
        {
            const string addressLine1 = "29 Acacia Road";
            var data = new Dictionary<string, object>
                           {
                               {"AddressLine1", addressLine1}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(addressLine1, member.AddressLine1);
        }

        [Test]
        public void ReturnAMemberWithTheAddressLine2PropertySet_WhenTheInputDataContainsAnAddressLine2Property()
        {
            const string addressLine2 = "Nuttytown";
            var data = new Dictionary<string, object>
                           {
                               {"AddressLine2", addressLine2}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(addressLine2, member.AddressLine2);
        }

        [Test]
        public void ReturnAMemberWithTheCityPropertySet_WhenTheInputDataContainsACityProperty()
        {
            const string city = "London";
            var data = new Dictionary<string, object>
                           {
                               {"City", city}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(city, member.City);
        }

        [Test]
        public void ReturnAMemberWithTheRegionPropertySet_WhenTheInputDataContainsARegionProperty()
        {
            const string region = "Greater London";
            var data = new Dictionary<string, object>
                           {
                               {"Region", region}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(region, member.Region);
        }

        [Test]
        public void ReturnAMemberWithThePostalCodePropertySet_WhenTheInputDataContainsAPostalCodeProperty()
        {
            const string postalCode = "W12 7RJ";
            var data = new Dictionary<string, object>
                           {
                               {"PostalCode", postalCode}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(postalCode, member.PostalCode);
        }

        [Test]
        public void ReturnAMemberWithTheCountryPropertySet_WhenTheInputDataContainsACountryProperty()
        {
            const string country = "Great Britain";
            var data = new Dictionary<string, object>
                           {
                               {"Country", country}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(country, member.Country);
        }
        
        [Test]
        public void ReturnAMemberWithTheArchivedPropertySet_WhenTheInputDataContainsAnArchivedProperty()
        {
            const bool archived = true;
            var data = new Dictionary<string, object>
                           {
                               {"Archived", archived}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(archived, member.Archived);
        }

        [Test]
        [TestCase]
        public void ReturnAMemberWithAllPropertiesSet_WhenTheInputDataContainsAllProperties()
        {
            const string reference = "SFJ458-AD2";
            const string title = "Mr";
            const string firstName = "Joe";
            const string lastName = "Bloggs";
            const string salutation = "Sir";
            const string emailAddress = "joe.bloggs@gmail.com";
            const string addressLine1 = "29 Acacia Road";
            const string addressLine2 = "Nuttytown";
            const string city = "London";
            const string region = "Greater London";
            const string postalCode = "W12 7RJ";
            const string country = "Great Britain";
            const bool archived = true;

            var data = new Dictionary<string, object>
                           {
                               {"Reference", reference},
                               {"Title", title},
                               {"FirstName", firstName},
                               {"LastName", lastName},
                               {"Salutation", salutation},
                               {"EmailAddress", emailAddress},
                               {"AddressLine1", addressLine1},
                               {"AddressLine2", addressLine2},
                               {"City", city},
                               {"Region", region},
                               {"PostalCode", postalCode},
                               {"Country", country},
                               {"Archived", archived}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(reference, member.Reference);
            Assert.AreEqual(title, member.Title);
            Assert.AreEqual(firstName, member.FirstName);
            Assert.AreEqual(lastName, member.LastName);
            Assert.AreEqual(salutation, member.Salutation);
            Assert.AreEqual(emailAddress, member.EmailAddress);
            Assert.AreEqual(addressLine1, member.AddressLine1);
            Assert.AreEqual(addressLine2, member.AddressLine2);
            Assert.AreEqual(city, member.City);
            Assert.AreEqual(region, member.Region);
            Assert.AreEqual(postalCode, member.PostalCode);
            Assert.AreEqual(country, member.Country);
            Assert.AreEqual(archived, member.Archived);
        }

    }
}
