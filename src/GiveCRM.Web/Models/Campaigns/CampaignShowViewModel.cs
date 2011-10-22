using GiveCRM.Models;

namespace GiveCRM.Web.Models.Campaigns
{
    public class CampaignShowViewModel : ViewModelBase
    {
        public Campaign Campaign { get; set; }

        public CampaignShowViewModel(string title) : base(title)
        {}
    }
}
