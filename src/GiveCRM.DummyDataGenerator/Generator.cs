using System.Collections.Generic;

namespace GiveCRM.DummyDataGenerator
{
    using System;

    using GiveCRM.DataAccess;
    using GiveCRM.DummyDataGenerator.Generation;
    using GiveCRM.Models;

    internal class Generator
    {
        private Campaign campaign;
        private List<Member> members;

        internal string GenerateMembers(int countToGenerate)
        {
            DateTime startTime = DateTime.Now;
            MemberGenerator generator = new MemberGenerator();
            
            members.Clear();
            members.Capacity = countToGenerate;
            var newMembers = generator.Generate(countToGenerate);

            Members membersDb = new Members();
            foreach (var member in newMembers)
            {
                Member saved = membersDb.Insert(member);
                members.Add(saved);
            }

            DateTime endTime = DateTime.Now;
            TimeSpan elapsedTime = endTime - startTime;
            return string.Format("{0} members saved in {1}", members.Count, elapsedTime);
        }

        internal string LoadMembers()
        {
            DateTime startTime = DateTime.Now;

            Members membersDb = new Members();

            members = new List<Member>(membersDb.All());

            DateTime endTime = DateTime.Now;
            TimeSpan elapsedTime = endTime - startTime;
            return string.Format("{0} members loaded in {1}", members.Count, elapsedTime);
            
        }

        internal string GenerateCampaign()
        {
            CampaignGenerator generator = new CampaignGenerator();
            campaign = generator.Generate();

            Campaigns campaignDb = new Campaigns();

            campaign = campaignDb.Insert(campaign);

            return "Generated campaign " + campaign;
        }

        internal string GenerateDonations()
        {
            DateTime startTime = DateTime.Now;
            DonationsGenerator generator = new DonationsGenerator(campaign, members);
            IList<Donation> newDonations = generator.Generate();

            Donations donationDb = new Donations();

            foreach (var donation in newDonations)
            {
                donationDb.Insert(donation);
            }

            DateTime endTime = DateTime.Now;
            TimeSpan elapsedTime = endTime - startTime;
            return string.Format("{0} donations inserted in {1}", newDonations.Count, elapsedTime);
        }
    }
}
