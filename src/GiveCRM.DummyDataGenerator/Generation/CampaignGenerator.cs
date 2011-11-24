using System;
using GiveCRM.DataAccess;
using GiveCRM.DummyDataGenerator.Data;
using GiveCRM.Models;

namespace GiveCRM.DummyDataGenerator.Generation
{
    internal class CampaignGenerator : BaseGenerator
    {
        internal override string GeneratedItemType{get {return "campaigns";}}

        private readonly RandomSource random = new RandomSource();

        public CampaignGenerator(Action<string> logAction) : base(logAction)
        {}

        internal override void Generate(int numberToGenerate)
        {
            Campaigns campaigns = new Campaigns();
            GenerateMultiple(numberToGenerate, GenerateCampaign, m => campaigns.Insert(m));
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

            //TODO: add random search criteria to campaigns, and add members if the campaign has been run
            return campaign;
        }
    }
}
