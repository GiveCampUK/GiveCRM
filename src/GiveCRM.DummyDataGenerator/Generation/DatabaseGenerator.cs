using System;
using System.Collections.Generic;
using System.Linq;
using GiveCRM.Models;
using Simple.Data;

namespace GiveCRM.DummyDataGenerator.Generation
{
    internal class DatabaseGenerator
    {
        private readonly RandomSource random = new RandomSource();
        private readonly dynamic db = Database.OpenNamedConnection("GiveCRM");
        private readonly Action<string> logAction;

        public DatabaseGenerator(Action<string> logAction)
        {
            this.logAction = logAction;
        }

        public void Generate()
        {
            EmptyDatabase();
            GenerateMembers();
            GenerateCampaigns();
            GenerateDonations();
            logAction("Database generated successfully");
        }

        private void EmptyDatabase()
        {
            logAction("Emptying database...");
            db.Donations.DeleteAll();
            db.CampaignRuns.DeleteAll();
            db.MemberSearchFilters.DeleteAll();
            db.Campaigns.DeleteAll();
            db.PhoneNumbers.DeleteAll();
            db.MemberFacetValues.DeleteAll();
            db.MemberFacets.DeleteAll();
            db.Members.DeleteAll();
            db.FacetValues.DeleteAll();
            db.Facets.DeleteAll();
        }

        private void GenerateMembers()
        {
            Generate(new MemberGenerator(logAction), 100, 1000);
//            Generate(new MemberGenerator(logAction), 1000, 10000);
        }

        private void GenerateCampaigns()
        {
            Generate(new CampaignGenerator(logAction), 15, 100);
        }

        private void GenerateDonations()
        {
            var campaigns = db.Campaigns.All().Cast<Campaign>();
/*
            IEnumerable<Member> members = db.Members.All().Cast<Member>();

            foreach (var campaign in campaigns)
            {
                int campaignId = campaign.Id;
                var campaignToMember = members.Select(member => new {CampaignId = campaignId, MemberId = member.Id});
                db.CampaignRuns.Insert(campaignToMember);
            }
*/
            new DonationGenerator(logAction, campaigns).Generate();
        }

        private void Generate(BaseGenerator generator, int minNumber, int maxNumber)
        {
            var numberToGenerate = random.NextInt(minNumber, maxNumber);
            generator.Generate(numberToGenerate);
        }
    }
}
