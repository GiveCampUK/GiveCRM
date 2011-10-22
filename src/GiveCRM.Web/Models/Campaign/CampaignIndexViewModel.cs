using System;
using System.Collections.Generic;

namespace GiveCRM.Web.Models.Campaign
{
    public class CampaignIndexViewModel
    {
        public string Title { get; set; }
        public bool ShowClosed { get; set; }
        public string ShowCampaignsLinkText { get; set; }
        public IEnumerable<GiveCRM.Models.Campaign> Campaigns { get; set; }
    }
}