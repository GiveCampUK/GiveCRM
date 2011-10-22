using System;
using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.Web.Models.Campaign
{
    public class CampaignIndexViewModel : ViewModelBase
    {
        public CampaignIndexViewModel(string title) : base(title)
        {
        }

        public bool ShowClosed { get; set; }
        public string ShowCampaignsLinkText { get; set; }
        public string CreateCampaignLinkText { get; set; }
        public IEnumerable<GiveCRM.Models.Campaign> Campaigns { get; set; }
    }
}