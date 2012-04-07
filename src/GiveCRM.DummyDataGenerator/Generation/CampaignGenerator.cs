using System;
using GiveCRM.DataAccess;
using GiveCRM.Models;
using GiveCRM.DummyDataGenerator.Data;

namespace GiveCRM.DummyDataGenerator.Generation
{
    internal class CampaignGenerator : BaseGenerator
    {
        internal override string GeneratedItemType
        {
            get { return "campaigns"; }
        }

        private readonly IDatabaseProvider databaseProvider;
        private readonly RandomSource random;
        private readonly MemberSearchFilterGenerator memberSearchFilterGenerator;
        private readonly CampaignRunGenerator campaignRunGenerator;
        
        public CampaignGenerator(Action<string> logAction) : base(logAction)
        {
            databaseProvider = new DatabaseProvider();
            random = new RandomSource();
            memberSearchFilterGenerator = new MemberSearchFilterGenerator();
            campaignRunGenerator = new CampaignRunGenerator(databaseProvider);
        }

        internal override void Generate(int numberToGenerate)
        {
            Campaigns campaigns = new Campaigns(databaseProvider);
            GenerateMultiple(numberToGenerate, () =>
                                                   {
                                                       var campaign = GenerateCampaign();
                                                       campaign = campaigns.Insert(campaign);
                                                       memberSearchFilterGenerator.GenerateMemberSearchFilters(campaign.Id);

                                                       if (campaign.IsCommitted)
                                                       {
                                                           // if the campaign is committed, the members that meet the campaign's search criteria also need creating
                                                           campaignRunGenerator.GenerateCampaignRun(campaign.Id);
                                                       }
                                                   });
        }

        private Campaign GenerateCampaign()
        {
            string campaignName = string.Format("{0} {1}", random.PickFromList(FemaleNames.Data), random.PickFromList(CampaignNames.CampaignSuffix));

            // close 10% of campaigns
            bool isClosed = random.Percent(10);

            // run (commit) 50% of campaigns, but all of the ones that are closed
            DateTime? runOn = isClosed || random.Percent(50) ? DateTime.Today : (DateTime?) null;

            var campaign = new Campaign
                               {
                                           Name = campaignName,
                                           Description = "A test campaign",
                                           IsClosed = isClosed  ? "Y" : "N",
                                           RunOn = runOn
                               };
            return campaign;
        }
    }
}
