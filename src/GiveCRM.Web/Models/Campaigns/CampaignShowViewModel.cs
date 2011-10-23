using System.Collections.Generic;
using System.Linq;
using GiveCRM.Models;

namespace GiveCRM.Web.Models.Campaigns
{
    public class CampaignShowViewModel : ViewModelBase
    {
        public Campaign Campaign { get; set; }
        public IEnumerable<MemberSearchFilterViewModel> SearchFilters { get; set; }
        public IEnumerable<Member> ApplicableMembers { get; set; }
        public string NoSearchFiltersText { get; set; }
        public string NoMatchingMembersText { get; set; }
        public IList<string> QuickLinks { get; set; }

        public CampaignShowViewModel() : this(string.Empty)
        {}

        public CampaignShowViewModel(string title) : base(title)
        {
            SearchFilters = Enumerable.Empty<MemberSearchFilterViewModel>();
            ApplicableMembers = Enumerable.Empty<Member>();
        }
    }
}
