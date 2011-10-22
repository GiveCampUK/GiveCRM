using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.Web.Models.Campaigns
{
    public class CampaignIndexViewModel : ViewModelBase
    {
        public bool ShowClosed { get; set; }
        public string ShowCampaignsLinkText { get; set; }
        public string CreateCampaignLinkText { get; set; }
        public IEnumerable<Campaign> Campaigns { get; set; }

        public CampaignIndexViewModel(string title) : base(title)
        {}
    }
}