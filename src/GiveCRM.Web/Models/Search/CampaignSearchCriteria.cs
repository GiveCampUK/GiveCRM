using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GiveCRM.Models;
using GiveCRM.DataAccess;

namespace GiveCRM.Web.Models.Search
{
    public class CampaignSearchCriteria : SearchCriteria
    {
        public static IEnumerable<SearchCriteria> GetEmptyCriteria()
        {
            yield return new CampaignSearchCriteria { InternalName = "donatedToCampaign", DisplayName = "Donated to campaign", Type = SearchFieldType.String };
        }

        public override bool IsMatch(Member m)
        {
            var donations = new GiveCRM.DataAccess.Donations();
            var campaigns = new GiveCRM.DataAccess.Campaigns();
            var thisMembersDonations = donations.ByMember(m.Id).Where(d => d.CampaignId!=null);
            switch (this.InternalName)
            {
                case "donatedToCampaign": return thisMembersDonations.Any(d => Evaluate(campaigns.Get(d.CampaignId.Value).Name));

                default:
                    return false;
            }
        }
    }
}