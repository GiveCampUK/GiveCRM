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

        internal string GenerateMembers()
        {
            DateTime startTime = DateTime.Now;
            MemberGenerator generator = new MemberGenerator();
            
            // taqrget data size = 100 000 members
            members = generator.Generate(100000);

            var membersDb = new Members();
            foreach (var member in members)
            {
                membersDb.Insert(member);
            }

            DateTime endTime = DateTime.Now;
            TimeSpan elapsedTime = endTime - startTime;
            return string.Format("{0} members saved in {1}", members.Count, elapsedTime);
        }

        internal string GenerateCampaign()
        {
            CampaignGenerator generator = new CampaignGenerator();
            campaign = generator.Generate();

            return "Generated campaign " + campaign;
        }

        internal string GenerateDonations()
        {
            DonationsGenerator generator = new DonationsGenerator(campaign, members);
            var donations = generator.Generate();

            return string.Format("{0} donations generated", donations.Count);
        }
    }
}
