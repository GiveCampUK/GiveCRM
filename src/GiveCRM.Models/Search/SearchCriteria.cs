using System;
using System.Collections.Generic;
using System.Linq;
using GiveCRM.Web.Models.Search;

namespace GiveCRM.Models.Search
{
    public abstract class SearchCriteria
    {
        public string InternalName { get; set; }
        public string DisplayName { get; set; }
        public SearchFieldType Type { get; set; }
        public SearchOperator SearchOperator { get; set; }
        public string Value { get; set; }

        protected bool Evaluate(object lhs)
        {
            object rhs = null;
            switch (this.Type)
            {
                case SearchFieldType.String: rhs=this.Value; break;
                case SearchFieldType.Int: rhs=int.Parse(this.Value); break;
                case SearchFieldType.Double: rhs = double.Parse(this.Value); break;
                case SearchFieldType.Date: rhs = DateTime.Parse(this.Value); break;
                default: rhs=this.Value; break;
            }

            switch (this.SearchOperator)
            {
                case SearchOperator.EqualTo: return lhs == rhs;
                case SearchOperator.NotEqualTo: return lhs != rhs;
                case SearchOperator.LessThan: return (this.Type == SearchFieldType.Int && (int)lhs < (int)rhs) || (this.Type == SearchFieldType.Double && (double)lhs < (double)rhs);
                case SearchOperator.GreaterThan: return (this.Type == SearchFieldType.Int && (int)lhs > (int)rhs) || (this.Type == SearchFieldType.Double && (double)lhs > (double)rhs);
                case SearchOperator.LessThanOrEqualTo: return (this.Type == SearchFieldType.Int && (int)lhs <= (int)rhs) || (this.Type == SearchFieldType.Double && (double)lhs <= (double)rhs);
                case SearchOperator.GreaterThanOrEqualTo: return (this.Type == SearchFieldType.Int && (int)lhs > (int)rhs) || (this.Type == SearchFieldType.Double && (double)lhs > (double)rhs);
                case SearchOperator.StartsWith: return (this.Type == SearchFieldType.Int && (int)lhs >= (int)rhs) || (this.Type == SearchFieldType.Double && (double)lhs >= (double)rhs);
                case SearchOperator.EndsWith: return lhs.ToString().EndsWith(rhs.ToString());
                case SearchOperator.Contains: return lhs.ToString().Contains(rhs.ToString());
                default: return false;
            }
        }

        public virtual IEnumerable<SearchOperator> GetAvailableOperators()
        {
            return Enum.GetValues(typeof (SearchOperator)).Cast<SearchOperator>();
        }

        public static SearchCriteria Create(string internalName, string displayName, SearchFieldType filterType, SearchOperator searchOperator, string value)
        {
            SearchCriteria criteria;
            if (CampaignSearchCriteria.IsCampaignSearchCriteria(internalName)) criteria = new CampaignSearchCriteria();
            else if (LocationSearchCriteria.IsLocationSearchCriteria(internalName)) criteria = new LocationSearchCriteria();
            else if (DonationSearchCriteria.IsDonationSearchCriteria(internalName)) criteria = new DonationSearchCriteria();
            else
            {
                if (internalName.StartsWith("freeTextFacet_"))
                {
                    int facetId;
                    if (int.TryParse(internalName.Substring(14), out facetId))
                    {
                        criteria = new FacetSearchCriteria {FacetId = facetId};
                    }
                }
            }

            criteria.InternalName = internalName;
            criteria.DisplayName = displayName;
            criteria.Type = filterType;
            criteria.SearchOperator = searchOperator;
            criteria.Value = value;

            return criteria;
        }
    }
}
