namespace GiveCRM.DummyDataGenerator
{
    internal class DatabaseStatistics
    {
        public int NumberOfMembers{get {return numberOfMembers;}}
        private readonly int numberOfMembers;

        public int NumberOfCampaigns{get {return numberOfCampaigns;}}
        private readonly int numberOfCampaigns;

        public int NumberOfSearchFilters{get {return numberOfSearchFilters;}}
        private readonly int numberOfSearchFilters;

        public int NumberOfDonations{get {return numberOfDonations;}}
        private readonly int numberOfDonations;

        public DatabaseStatistics(int numberOfMembers, int numberOfCampaigns, int numberOfSearchFilters, int numberOfDonations)
        {
            this.numberOfMembers = numberOfMembers;
            this.numberOfCampaigns = numberOfCampaigns;
            this.numberOfSearchFilters = numberOfSearchFilters;
            this.numberOfDonations = numberOfDonations;
        }
    }
}
