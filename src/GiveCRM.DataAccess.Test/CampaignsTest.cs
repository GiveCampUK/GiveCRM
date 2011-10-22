using System;
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
            _db.MemberFacetValues.DeleteAll();
            _db.MemberFacets.DeleteAll();
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
    }
}
