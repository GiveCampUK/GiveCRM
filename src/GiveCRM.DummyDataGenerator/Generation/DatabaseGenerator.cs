using System;
using Simple.Data;

namespace GiveCRM.DummyDataGenerator.Generation
{
    internal class DatabaseGenerator
    {
        private readonly dynamic db;
        private readonly Action<string> logAction;

        public DatabaseGenerator(string connectionString, Action<string> logAction)
        {
            this.db = Database.OpenConnection(connectionString);
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
            logAction("Generating members...");
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
