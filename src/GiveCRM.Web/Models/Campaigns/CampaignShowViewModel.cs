using System.Collections.Generic;
using System.Linq;
using GiveCRM.Models;

namespace GiveCRM.Web.Models.Campaigns
{
    public class CampaignShowViewModel : ViewModelBase
    {
        public Campaign Campaign { get; set; }
        public IEnumerable<MemberSearchFilterViewModel> SearchFilters { get; set; }
        public string NoSearchFiltersText { get; set; }

        public CampaignShowViewModel() : this(string.Empty)
        {}

        public CampaignShowViewModel(string title) : base(title)
        {
            SearchFilters = Enumerable.Empty<MemberSearchFilterViewModel>();
        }
    }
}
