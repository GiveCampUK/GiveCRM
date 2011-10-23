namespace GiveCRM.Web.Models.Campaigns
{
    public class SimpleCampaignViewModel : ViewModelBase
    {
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }

        public SimpleCampaignViewModel() : this(string.Empty)
        {}

        public SimpleCampaignViewModel(string title) : base(title)
        {}
    }
}