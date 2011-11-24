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
            Generate(new MemberGenerator(logAction), 1000, 10000);
        }

        private void GenerateCampaigns()
        {
            Generate(new CampaignGenerator(logAction), 15, 100);
        }

        private void GenerateDonations()
        {
            logAction("Generating donations...");
        }

        private void Generate(BaseGenerator generator, int minNumber, int maxNumber)
        {
            logAction(string.Format("Generating {0}...", generator.GeneratedItemType));
            var numberToGenerate = random.Next(minNumber, maxNumber);
            generator.Generate(numberToGenerate);
        }
    }
}
