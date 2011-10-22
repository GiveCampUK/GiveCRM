using System;
using System.Collections.Generic;
using System.Linq;
using GiveCRM.Models;
using GiveCRM.DataAccess;

namespace GiveCRM.Web.Models.Search
{
    public class SearchService
    {
        public IEnumerable<SearchCriteria> GetSearchCriteria()
        {
            var criteria = new List<SearchCriteria>();
            criteria.AddRange(DonationSearchCriteria.GetEmptyCriteria());
            criteria.AddRange(CampaignSearchCriteria.GetEmptyCriteria());
            return criteria;
        }

        public IEnumerable<Member> SearchForMembers(IEnumerable<SearchCriteria> criteria)
        {
            var members = new GiveCRM.DataAccess.Members();
            return members.All().Where(m => criteria.All(c => c.IsMatch(m)));
        }
    }
}