using System.Collections.Generic;
using GiveCRM.Web.Models.Search;

namespace GiveCRM.Models.Search
{
    public class CampaignSearchCriteria : SearchCriteria
    {
        public const string DonatedToCampaign = "donatedToCampaign";

        public static bool IsCampaignSearchCriteria(string internalName)
        {
            return internalName.Equals(DonatedToCampaign);
        }
    }
}