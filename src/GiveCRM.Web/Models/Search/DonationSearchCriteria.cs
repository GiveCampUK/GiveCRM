using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GiveCRM.Models;
using GiveCRM.DataAccess;

namespace GiveCRM.Web.Models.Search
{
    public class DonationSearchCriteria : SearchCriteria
    {
        private Donations donations = new Donations();

        public static IEnumerable<SearchCriteria> GetEmptyCriteria()
        {
            yield return new DonationSearchCriteria { InternalName = "individualDonation", DisplayName = "Individual donation", Type = SearchFieldType.Double };
            yield return new DonationSearchCriteria { InternalName = "totalDonations", DisplayName = "Total donations", Type = SearchFieldType.Double };
            yield return new DonationSearchCriteria { InternalName = "lastDonationDate", DisplayName = "Last donation date", Type = SearchFieldType.Date };
            yield return new DonationSearchCriteria { InternalName = "averageDonationsPerYear", DisplayName = "Average donations per year", Type = SearchFieldType.Int};
        }

        public override bool IsMatch(Member m)
        {
            var thisMembersDonations = donations.ByMember(m.Id);
            switch (this.InternalName)
            {
                case "individualDonation": return thisMembersDonations.Any(x => Evaluate(x));
                case "totalDonations": return Evaluate(thisMembersDonations.Sum(x => x.Amount));
                case "lastDonationDate": return Evaluate(thisMembersDonations.OrderBy(x=>x.Date).Last());
                case "averageDonationsPerYear": return Evaluate(thisMembersDonations.GroupBy(x => x.Date.Year).Average(x => x.Sum(y => y.Amount)));
                default: return false;
            }
        }
    }
}