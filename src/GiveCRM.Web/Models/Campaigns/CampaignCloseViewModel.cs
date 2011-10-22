namespace GiveCRM.Web.Models.Campaigns
{
    public class CampaignCloseViewModel : ViewModelBase
    {
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }

        public CampaignCloseViewModel() : this(string.Empty)
        {}

        public CampaignCloseViewModel(string title) : base(title)
        {}
    }
}