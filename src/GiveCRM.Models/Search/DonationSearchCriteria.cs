using System.Collections.Generic;
using GiveCRM.Web.Models.Search;

namespace GiveCRM.Models.Search
{
    public class DonationSearchCriteria : SearchCriteria
    {
        public const string IndividualDonation = "individualDonation";
        public const string TotalDonations = "totalDonations";
        public const string LastDonationDate = "lastDonationDate";
        public const string AverageDonationsPerYear = "averageDonationsPerYear";

        private static readonly HashSet<string> Names = new HashSet<string>
                                                            {
                                                                IndividualDonation,
                                                                TotalDonations,
                                                                LastDonationDate,
                                                                AverageDonationsPerYear
                                                            }; 

        public static bool IsDonationSearchCriteria(string internalName)
        {
            return Names.Contains(internalName);
        }
    }
}