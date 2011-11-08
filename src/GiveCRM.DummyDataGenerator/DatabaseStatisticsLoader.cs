namespace GiveCRM.DummyDataGenerator
{
    internal class DatabaseStatisticsLoader
    {
        public DatabaseStatistics Load(dynamic db)
        {
            int numberOfMembers = db.Members.Query().Count();
            int numberOfCampaigns = db.Campaigns.Query().Count();
            int numberOfDonations = db.Donations.Query().Count();
            return new DatabaseStatistics(numberOfMembers, numberOfCampaigns, numberOfDonations);
        }
    }

    internal class DatabaseStatistics
    {
        public int NumberOfMembers{get {return numberOfMembers;}}
        private readonly int numberOfMembers;

        public int NumberOfCampaigns{get {return numberOfCampaigns;}}
        private readonly int numberOfCampaigns;

        public int NumberOfDonations{get {return numberOfDonations;}}
        private readonly int numberOfDonations;

        public DatabaseStatistics(int numberOfMembers, int numberOfCampaigns, int numberOfDonations)
        {
            this.numberOfMembers = numberOfMembers;
            this.numberOfCampaigns = numberOfCampaigns;
            this.numberOfDonations = numberOfDonations;
        }
    }

}