namespace GiveCRM.DummyDataGenerator
{
    internal class DatabaseStatisticsLoader
    {
        public DatabaseStatistics Load(dynamic db)
        {
            int numberOfMembers = db.Members.Query().Count();
            int numberOfCampaigns = db.Campaigns.Query().Count();
            int numberOfSearchFilters = db.MemberSearchFilters.Query().Count();
            int numberOfDonations = db.Donations.Query().Count();
            return new DatabaseStatistics(numberOfMembers, numberOfCampaigns, numberOfSearchFilters, numberOfDonations);
        }
    }
}
