namespace GiveCRM.DummyDataGenerator
{
    using System;

    using GiveCRM.DummyDataGenerator.Data;
    using GiveCRM.Models;
    using GiveCRM.DummyDataGenerator.Generation;

    internal class CampaignGenerator
    { 
        private readonly RandomSource random = new RandomSource();

        public Campaign Generate()
        {
            string campaignName = random.PickFromList(NameData.FemaleFirstNames) + " " +
                                  random.PickFromList(CampaignNames.CampaignSuffix);
            return new Campaign
                {
                    Name = campaignName,
                    Description = "The " + campaignName + " is test data",
                    IsClosed = 'N',
                    RunOn = DateTime.Today
                };
        }
    }
}