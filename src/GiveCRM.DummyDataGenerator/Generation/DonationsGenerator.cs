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
        private int donationRate;

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

        internal IList<Donation> Generate()
        {
            var donations = new List<Donation>();

            foreach (var member in members)
            {
                if (random.Percent(donationRate))
                {
                    donations.Add(DonationForMember(member));
                }   
            }

            return donations;
        }

        private Donation DonationForMember(Member member)
        {
            DateTime backDate = DateTime.Today.AddDays(-1 * random.Next(100));
            decimal randomAmount = (decimal)5 + random.Next(96);
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