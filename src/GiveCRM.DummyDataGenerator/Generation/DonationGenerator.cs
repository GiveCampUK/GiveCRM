using System;
using System.Collections.Generic;
using GiveCRM.DataAccess;
using GiveCRM.Models;
using System.Linq;

namespace GiveCRM.DummyDataGenerator.Generation
{
    internal class DonationGenerator
    {
        private readonly RandomSource random = new RandomSource();
        private readonly Action<string> logAction;
        private readonly IEnumerable<Campaign> campaigns;
        private readonly int donationRate;

        public DonationGenerator(Action<string> logAction, IEnumerable<Campaign> campaigns) 
        {
            this.logAction = logAction;
            this.campaigns = campaigns;

            // donation rate is set somewhere between 1/3 and 2/3
            donationRate = 33 + random.NextInt(33);
        }

        internal void Generate()
        {
            logAction("Generating donations...");
            //TODO: output percentage completion like the other generators

            // only want to generate donations for committed campaigns
            var committedCampaigns = campaigns.Where(c => c.IsCommitted).ToList();
            var databaseProvider = new SingleTenantDatabaseProvider();
            var members = new Members(databaseProvider);
            var donations = new Donations(databaseProvider);

            foreach (var campaign in committedCampaigns)
            {
                var membersForCampaign = members.GetByCampaignId(campaign.Id);

                foreach (var member in membersForCampaign)
                {
                    GenerateCampaignDonationsForMember(donations, campaign, member);
                }
            }

            logAction("Donations generated successfully");
        }

        private void GenerateCampaignDonationsForMember(Donations donationsRepo, Campaign campaign, Member member)
        {
            int numberOfDonations = random.NextInt(0, 3);

            for (int i = 0; i < numberOfDonations; i++)
            {
                // don't generate donations each time to simulate a miss
                if (this.random.Percent(donationRate))
                {
                    var amount = GenerateAmount();
                    var donation = new Donation
                                       {
                                            CampaignId = campaign.Id, 
                                            MemberId = member.Id,
                                            Amount = amount, 
                                            Date = random.NextDateTime(),
                                       };
                    donationsRepo.Insert(donation);
                }
            }
        }

        private decimal GenerateAmount()
        {
            int poundsAmount = random.NextInt(100);
            int penceAmount = 0;

            // high chance that there is no fractional amount
            if (!random.Percent(75))
            {
                // chance that it's a quarter, half or three quarters
                if (random.Percent(50))
                {
                    penceAmount = Convert.ToInt32((random.NextInt(4) / 4.0) * 100);
                }
                else
                {
                    // random from .01 to 0.99
                    penceAmount = random.NextInt(100);
                }
            }

            int totalAmount = (poundsAmount * 100) + penceAmount;
            return new Decimal(totalAmount, 0, 0, false, 2);
        }
    }
}
