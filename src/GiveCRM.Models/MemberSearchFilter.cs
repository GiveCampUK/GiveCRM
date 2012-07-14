﻿namespace GiveCRM.Models
{
    using GiveCRM.Models.Search;
    using GiveCRM.Web.Models.Search;

    public class MemberSearchFilter
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }

        public string InternalName { get; set; }
        public string DisplayName { get; set; }
        public int FilterType { get; set; }
        public int SearchOperator { get; set; }
        public string Value { get; set; }

        public SearchCriteria ToSearchCriteria()
        {
            return SearchCriteria.Create(InternalName, DisplayName, (SearchFieldType) FilterType, (SearchOperator) SearchOperator, Value);
        }
    }
}
