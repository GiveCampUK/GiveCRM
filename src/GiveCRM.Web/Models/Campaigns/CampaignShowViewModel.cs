using System;
using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.Web.Models.Campaigns
{
    public class CampaignShowViewModel : ViewModelBase
    {
        public Campaign Campaign { get; set; }

        public IEnumerable<MemberSearchFilter> SearchFilters { get; set; }

        public string NoSearchFiltersText { get; set; }

        public CampaignShowViewModel(string title) : base(title)
        {}
    }
}
