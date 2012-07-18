namespace GiveCRM.DataAccess.Test
{
    using GiveCRM.Models;
    using NUnit.Framework;
    using Simple.Data;

    [TestFixture]
    public class CampaignsTest
    {
        private InMemorySingleTenantDatabaseProvider databaseProvider;

        [SetUp]
        public void SetUp()
        {
            databaseProvider = new InMemorySingleTenantDatabaseProvider();
            databaseProvider.Adapter.SetAutoIncrementKeyColumn("Campaigns", "Id");
            dynamic db = databaseProvider.GetDatabase();
            db.Donations.DeleteAll();
            db.CampaignRuns.DeleteAll();
            db.Campaigns.DeleteAll();
            db.PhoneNumbers.DeleteAll();
            db.MemberFacetValues.DeleteAll();
            db.MemberFacets.DeleteAll();
            db.Members.DeleteAll();
        }

        [Test]
        public void InsertCampaign()
        {
            var campaigns = new Campaigns(databaseProvider);
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
