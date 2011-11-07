using System.Collections.Generic;
using GiveCRM.BusinessLogic.ExcelImport;
using GiveCRM.Models;
using NUnit.Framework;

namespace GiveCRM.Web.Tests.Services.ExcelImport
{
    [TestFixture]
    public class MemberFactory_CreateMember_Should
    {
        private IMemberFactory factory;

        [SetUp]
        public void Setup()
        {
            factory = new MemberFactory();
        }

        [Test]
        public void ReturnAMemberWithTheReferencePropertySet_WhenTheInputDataContainsAReferenceProperty()
        {
            const string Reference = "SFJ458-AD2";
            var data = new Dictionary<string, object>
                           {
                               {"Reference", Reference}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(Reference, member.Reference);
        }

        [Test]
        public void ReturnAMemberWithTheTitlePropertySet_WhenTheInputDataContainsATitleProperty()
        {
            const string Title = "Mr";
            var data = new Dictionary<string, object>
                           {
                               {"Title", Title}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(Title, member.Title);
        }

        [Test]
        public void ReturnAMemberWithTheFirstNamePropertySet_WhenTheInputDataContainsAFirstNameProperty()
        {
            const string FirstName = "Joe";
            var data = new Dictionary<string, object>
                           {
                               {"FirstName", FirstName}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(FirstName, member.FirstName);
        }

        [Test]
        public void ReturnAMemberWithTheLastNamePropertySet_WhenTheInputDataContainsALastNameProperty()
        {
            const string LastName = "Bloggs";
            var data = new Dictionary<string, object>
                           {
                               {"LastName", LastName}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(LastName, member.LastName);
        }

        [Test]
        public void ReturnAMemberWithTheSalutationPropertySet_WhenTheInputDataContainsASalutationProperty()
        {
            const string Salutation = "Sir";
            var data = new Dictionary<string, object>
                           {
                               {"Salutation", Salutation}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(Salutation, member.Salutation);
        }

        [Test]
        public void ReturnAMemberWithTheEmailAddressPropertySet_WhenTheInputDataContainsAnEmailAddressProperty()
        {
            const string EmailAddress = "joe.bloggs@gmail.com";
            var data = new Dictionary<string, object>
                           {
                               {"EmailAddress", EmailAddress}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(EmailAddress, member.EmailAddress);
        }

        [Test]
        public void ReturnAMemberWithTheAddressLine1PropertySet_WhenTheInputDataContainsAnAddressLine1Property()
        {
            const string AddressLine1 = "29 Acacia Road";
            var data = new Dictionary<string, object>
                           {
                               {"AddressLine1", AddressLine1}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(AddressLine1, member.AddressLine1);
        }

        [Test]
        public void ReturnAMemberWithTheAddressLine2PropertySet_WhenTheInputDataContainsAnAddressLine2Property()
        {
            const string AddressLine2 = "Nuttytown";
            var data = new Dictionary<string, object>
                           {
                               {"AddressLine2", AddressLine2}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(AddressLine2, member.AddressLine2);
        }

        [Test]
        public void ReturnAMemberWithTheCityPropertySet_WhenTheInputDataContainsACityProperty()
        {
            const string City = "London";
            var data = new Dictionary<string, object>
                           {
                               {"City", City}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(City, member.City);
        }

        [Test]
        public void ReturnAMemberWithTheRegionPropertySet_WhenTheInputDataContainsARegionProperty()
        {
            const string Region = "Greater London";
            var data = new Dictionary<string, object>
                           {
                               {"Region", Region}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(Region, member.Region);
        }

        [Test]
        public void ReturnAMemberWithThePostalCodePropertySet_WhenTheInputDataContainsAPostalCodeProperty()
        {
            const string PostalCode = "W12 7RJ";
            var data = new Dictionary<string, object>
                           {
                               {"PostalCode", PostalCode}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(PostalCode, member.PostalCode);
        }

        [Test]
        public void ReturnAMemberWithTheCountryPropertySet_WhenTheInputDataContainsACountryProperty()
        {
            const string Country = "Great Britain";
            var data = new Dictionary<string, object>
                           {
                               {"Country", Country}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(Country, member.Country);
        }
        
        [Test]
        public void ReturnAMemberWithTheArchivedPropertySet_WhenTheInputDataContainsAnArchivedProperty()
        {
            const bool Archived = true;
            var data = new Dictionary<string, object>
                           {
                               {"Archived", Archived}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(Archived, member.Archived);
        }

        [Test]
        [TestCase]
        public void ReturnAMemberWithAllPropertiesSet_WhenTheInputDataContainsAllProperties()
        {
            const string Reference = "SFJ458-AD2";
            const string Title = "Mr";
            const string FirstName = "Joe";
            const string LastName = "Bloggs";
            const string Salutation = "Sir";
            const string EmailAddress = "joe.bloggs@gmail.com";
            const string AddressLine1 = "29 Acacia Road";
            const string AddressLine2 = "Nuttytown";
            const string City = "London";
            const string Region = "Greater London";
            const string PostalCode = "W12 7RJ";
            const string Country = "Great Britain";
            const bool Archived = true;

            var data = new Dictionary<string, object>
                           {
                               {"Reference", Reference},
                               {"Title", Title},
                               {"FirstName", FirstName},
                               {"LastName", LastName},
                               {"Salutation", Salutation},
                               {"EmailAddress", EmailAddress},
                               {"AddressLine1", AddressLine1},
                               {"AddressLine2", AddressLine2},
                               {"City", City},
                               {"Region", Region},
                               {"PostalCode", PostalCode},
                               {"Country", Country},
                               {"Archived", Archived}
                           };

            Member member = factory.CreateMember(data);

            Assert.AreEqual(Reference, member.Reference);
            Assert.AreEqual(Title, member.Title);
            Assert.AreEqual(FirstName, member.FirstName);
            Assert.AreEqual(LastName, member.LastName);
            Assert.AreEqual(Salutation, member.Salutation);
            Assert.AreEqual(EmailAddress, member.EmailAddress);
            Assert.AreEqual(AddressLine1, member.AddressLine1);
            Assert.AreEqual(AddressLine2, member.AddressLine2);
            Assert.AreEqual(City, member.City);
            Assert.AreEqual(Region, member.Region);
            Assert.AreEqual(PostalCode, member.PostalCode);
            Assert.AreEqual(Country, member.Country);
            Assert.AreEqual(Archived, member.Archived);
        }

    }
}
