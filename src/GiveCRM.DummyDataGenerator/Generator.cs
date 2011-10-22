using System.Collections.Generic;

namespace GiveCRM.DummyDataGenerator
{
    using GiveCRM.DataAccess;
    using GiveCRM.DummyDataGenerator.Generation;
    using GiveCRM.Models;

    internal class Generator
    {
        private Campaign campaign;
        private List<Member> members;

        internal string GenerateMembers()
        {
            MemberGenerator generator = new MemberGenerator();
            members = generator.Generate(1000);

            var membersDb = new Members();
            foreach (var member in members)
            {
                membersDb.Insert(member);
            }

            return string.Format("{0} members saved", members.Count);
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
