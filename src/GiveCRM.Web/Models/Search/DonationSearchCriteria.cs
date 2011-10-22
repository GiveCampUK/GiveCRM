using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GiveCRM.Models;

namespace GiveCRM.Web.Models.Search
{
    public class DonationSearchCriteria : SearchCriteria
    {
        public override IEnumerable<SearchCriteria> GetEmptyCriteria()
        {
            yield return new DonationSearchCriteria { InternalName = "individualDonation", DisplayName = "Individual donation", Type = SearchFieldType.Number };
            yield return new DonationSearchCriteria { InternalName = "totalDonations", DisplayName = "Total donations", Type = SearchFieldType.Number };
            yield return new DonationSearchCriteria { InternalName = "lastDonationDate", DisplayName = "Last donation date", Type = SearchFieldType.Date };
        }

        public override bool IsMatch(Member m)
        {
            switch (this.InternalName)
            {
                case "individualDonation":
                   
                                    case "totalDonations":

                case "lastDonationDate":

                default:
                    return false;
            }
        }
    }
}