using System;
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
            db.Campaigns.DeleteAll();
            db.PhoneNumbers.DeleteAll();
            db.MemberSearchFilters.DeleteAll();
            db.MemberFacetValues.DeleteAll();
            db.MemberFacets.DeleteAll();
            db.Members.DeleteAll();
            db.FacetValues.DeleteAll();
            db.Facets.DeleteAll();
        }

        private void GenerateMembers()
        {
            MemberGenerator generator = new MemberGenerator(logAction);
            var numberToGenerate = random.Next(1000, 10000);
            generator.GenerateMembers(numberToGenerate);
        }

        private void GenerateCampaigns()
        {
            logAction("Generating campaigns...");
        }

        private void GenerateDonations()
        {
            logAction("Generating donations...");
        }
    }
}
