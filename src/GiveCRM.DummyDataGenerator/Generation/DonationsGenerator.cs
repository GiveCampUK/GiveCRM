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

        internal IList<Donation> Generate(int minAmount, int maxAmount)
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
                if (random.Percent(donationRate))
                {
                    donations.Add(DonationForMember(member, minAmount, maxAmount));
                }   
            }

            return donations;
        }

        private Donation DonationForMember(Member member, int minAmount, int maxAmount)
        {
            DateTime backDate = DateTime.Today.AddDays(-1 * random.Next(100));
            int randomAmount = random.Next(minAmount, maxAmount + 1);
            return new Donation
                {
                    CampaignId = campaign.Id,
                    MemberId = member.Id,
                    Date = backDate,
                    Amount = randomAmount
                };
        }
    }
}