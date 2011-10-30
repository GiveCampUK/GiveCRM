namespace GiveCRM.DummyDataGenerator.Generation
{
    using System;
    using System.Collections.Generic;

    using GiveCRM.Models;

    internal class DonationsGenerator
    {
        private readonly Campaign campaign;
        private readonly ICollection<Member> members;

        private readonly RandomSource random = new RandomSource();
        private readonly int donationRate;

        public DonationsGenerator(Campaign campaign, ICollection<Member> members)
        {
            if (campaign == null)
            {
                throw new ArgumentException("campaign");
            }

            if (members == null)
            {
                throw new ArgumentException("members");
            }

            this.campaign = campaign;
            this.members = members;

            // donation rate is set somewhere between 1/3 and 2/3
            donationRate = 33 + random.Next(33);
        }

        internal IList<Donation> Generate(int minAmount, int maxAmount, int donationCountMax)
        {
            if (minAmount <= 0)
            {
                throw new ArgumentException("Minimum donation amount must be positive");
            }

            if (maxAmount < minAmount)
            {
                throw new ArgumentException("Maximum donation amount is less than minimum amount");
            } 
            
            var donations = new List<Donation>();

            foreach (var member in members)
            {
                this.GenerateDonationsForMember(minAmount, maxAmount, donationCountMax, member, donations);
            }

            return donations;
        }

        private void GenerateDonationsForMember(int minAmount, int maxAmount, int donationCountMax, 
            Member member, List<Donation> donations)
        {
            for (int donationsForMember = 0; donationsForMember < donationCountMax; donationsForMember++)
            {
                if (this.random.Percent(this.donationRate))
                {
                    donations.Add(this.DonationForMember(member, minAmount, maxAmount));
                }
            }
        }

        private Donation DonationForMember(Member member, int minAmount, int maxAmount)
        {
            DateTime backDate = DateTime.Today.AddDays(-1 * random.Next(100));
            int amountInt = random.Next(minAmount, maxAmount + 1);

            decimal donationAmount = amountInt;
            if (donationAmount < maxAmount)
            {
                donationAmount += RandomFaction();
            }
            
            return new Donation
                {
                    CampaignId = campaign.Id,
                    MemberId = member.Id,
                    Date = backDate,
                    Amount = donationAmount
                };
        }

        private decimal RandomFaction()
        {
            // high cance that there is no factional amount
            if (random.Percent(75))
            {
                return 0m;
            }

            // chance that it's a quarter, half or three quarters
            if (random.Percent(50))
            {
                return (decimal)random.Next(4) / 4;
            }

            // random from .01 to 0.99
            return (decimal)random.Next(100) / 100;
        }
    }
}