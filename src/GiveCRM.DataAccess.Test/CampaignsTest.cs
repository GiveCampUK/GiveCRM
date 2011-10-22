using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveCRM.Models;
using NUnit.Framework;
using Simple.Data;

namespace GiveCRM.DataAccess.Test
{
    [TestFixture]
    public class CampaignsTest
    {
        private readonly dynamic _db = Database.OpenNamedConnection("GiveCRM");

        [SetUp]
        public void SetUp()
        {
            _db.Donations.DeleteAll();
            _db.CampaignRuns.DeleteAll();
            _db.Campaigns.DeleteAll();
            _db.PhoneNumbers.DeleteAll();
            _db.Members.DeleteAll();
        }

        [Test]
        public void InsertCampaign()
        {
            var campaigns = new Campaigns();
            var campaign = new Campaign
                               {
                                   Name = "Test",
                                   Description = "Test"
                               };
            campaign = campaigns.Insert(campaign);
            Assert.AreNotEqual(0, campaign.Id);
            Assert.AreEqual("N", campaign.IsClosed);
        }

        [Test]
        public void InsertMember()
        {
            var members = new Members();
            var member = CreateAlice();
            member = members.Insert(member);
            Assert.AreNotEqual(0, member.Id);
        }

        [Test]
        public void InsertMemberWithPhoneNumber()
        {
            var members = new Members();
            var member = CreateAlice();
            member.PhoneNumbers = new List<PhoneNumber>
                                      {
                                          new PhoneNumber
                                              {
                                                  Type = "Home",
                                                  Number = "01234 567890"
                                              }
                                      };
            member = members.Insert(member);
            Assert.AreNotEqual(0, member.Id);
            Assert.AreEqual(1, member.PhoneNumbers.Count);
            Assert.AreEqual(member.Id, member.PhoneNumbers.First().MemberId);
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
                                 AddressLine1 = "Dunassimilating",
                                 AddressLine2 = "Sector 4",
                                 City = "Nexus One",
                                 Region = "Delta Quadrant",
                                 Country = "Wales",
                                 PostalCode = "CA1 0PP",
                             };
            return member;
        }
    }
}
