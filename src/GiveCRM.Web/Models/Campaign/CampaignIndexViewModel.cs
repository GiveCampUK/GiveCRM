using System;
using System.Collections.Generic;

namespace GiveCRM.Web.Models.Campaign
{
    public class CampaignIndexViewModel
    {
        public IEnumerable<GiveCRM.Models.Campaign> Campaigns { get; set; }
    }
}