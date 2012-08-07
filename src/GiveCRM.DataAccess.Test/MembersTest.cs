namespace GiveCRM.DataAccess.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GiveCRM.Models;
    using NUnit.Framework;

    [TestFixture]
    [Ignore("Ignored until a new version of Simple.Data is available to fix issues with InMemoryAdapter.")]
    public class MembersTest
    {
        [SetUp]
        public void SetUp()
        {
            databaseProvider = new InMemorySingleTenantDatabaseProvider();
            dynamic db = databaseProvider.GetDatabase();
            db.Donations.DeleteAll();
            db.CampaignRuns.DeleteAll();
            db.Campaigns.DeleteAll();
            db.PhoneNumbers.DeleteAll();
            db.MemberFacetValues.DeleteAll();
            db.MemberFacets.DeleteAll();
            db.Members.DeleteAll();
        }

        private IDatabaseProvider databaseProvider;

        private static Member CreateAliceWithPhoneNumber()
        {
            Member member = CreateAlice();
            member.PhoneNumbers = new List<PhoneNumber>
                                      {
                                          new PhoneNumber
                                              {
                                                  PhoneNumberType = PhoneNumberType.Home,
                                                  Number = "01234 567890"
                                              }
                                      };
            return member;
        }

        private static Member CreateAlice()
        {
            var member = new Member
                             {
                                 Reference = "ABC",
                                 Title = "Ms",
                                 FirstName = "Alice",
                                 LastName = "Krige",
                                 Salutation = "Alice",
                                 EmailAddress = "alice@hotmail.com",
                                 AddressLine1 = "Dunassimilatin",
                                 AddressLine2 = "Sector 4",
                                 City = "Nexus One",
                                 Region = "Delta Quadrant",
                                 Country = "Wales",
                                 PostalCode = "CA1 0PP",
                             };
            return member;
        }

        [Test]
        public void Get()
        {
            var members = new Members(databaseProvider);
            Member member = CreateAliceWithPhoneNumber();
            member = members.Insert(member);

            member = members.GetById(member.Id);
            Assert.AreEqual("ABC", member.Reference);
            Assert.AreEqual("Ms", member.Title);
            Assert.AreEqual("Alice", member.FirstName);
            Assert.AreEqual("Krige", member.LastName);
            Assert.AreEqual("Alice", member.Salutation);
            Assert.AreEqual("alice@hotmail.com", member.EmailAddress);
            Assert.AreEqual("Dunassimilatin", member.AddressLine1);
            Assert.AreEqual("Sector 4", member.AddressLine2);
            Assert.AreEqual("Nexus One", member.City);
            Assert.AreEqual("Delta Quadrant", member.Region);
            Assert.AreEqual("Wales", member.Country);
            Assert.AreEqual("CA1 0PP", member.PostalCode);

            Assert.AreEqual(1, member.PhoneNumbers.Count);
            PhoneNumber phone = member.PhoneNumbers.First();
            Assert.AreEqual(member.Id, phone.MemberId);
            Assert.AreEqual(PhoneNumberType.Home, phone.PhoneNumberType);
            Assert.AreEqual("01234 567890", phone.Number);
        }

        [Test]
        public void All()
        {
            var members = new Members(databaseProvider);
            Member member = CreateAliceWithPhoneNumber();
            member = members.Insert(member);
            var donations = new Donations(databaseProvider);
            donations.Insert(new Donation {MemberId = member.Id, Amount = 12.50m, Date = DateTime.Today});
            donations.Insert(new Donation { MemberId = member.Id, Amount = 12.50m, Date = DateTime.Today.Subtract(TimeSpan.FromDays(1)) });

            var list = members.GetAll().ToList();
            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);
            member = list.FirstOrDefault();
            Assert.IsNotNull(member);
            Assert.AreEqual("ABC", member.Reference);
            Assert.AreEqual("Ms", member.Title);
            Assert.AreEqual("Alice", member.FirstName);
            Assert.AreEqual("Krige", member.LastName);
            Assert.AreEqual("Alice", member.Salutation);
            Assert.AreEqual("alice@hotmail.com", member.EmailAddress);
            Assert.AreEqual("Dunassimilatin", member.AddressLine1);
            Assert.AreEqual("Sector 4", member.AddressLine2);
            Assert.AreEqual("Nexus One", member.City);
            Assert.AreEqual("Delta Quadrant", member.Region);
            Assert.AreEqual("Wales", member.Country);
            Assert.AreEqual("CA1 0PP", member.PostalCode);
            Assert.AreEqual(25m, member.TotalDonations);

            Assert.AreEqual(1, member.PhoneNumbers.Count);
            PhoneNumber phone = member.PhoneNumbers.First();
            Assert.AreEqual(member.Id, phone.MemberId);
            Assert.AreEqual(PhoneNumberType.Home, phone.PhoneNumberType);
            Assert.AreEqual("01234 567890", phone.Number);
        }

        [Test]
        public void InsertMember()
        {
            var members = new Members(databaseProvider);
            Member member = CreateAlice();
            member = members.Insert(member);
            Assert.AreNotEqual(0, member.Id);
        }

        [Test]
        public void InsertMemberWithPhoneNumber()
        {
            var members = new Members(databaseProvider);
            Member member = CreateAliceWithPhoneNumber();
            member = members.Insert(member);
            Assert.AreNotEqual(0, member.Id);
            Assert.AreEqual(1, member.PhoneNumbers.Count);
            Assert.AreEqual(member.Id, member.PhoneNumbers.First().MemberId);
        }
    }
}