using System.Collections.Generic;

using GiveCRM.DataAccess;
using GiveCRM.Models;
using GiveCRM.Models.Search;

namespace GiveCRM.Web.Services
{
    public class SearchService: ISearchService
    {
        private readonly Search searchRepo = new Search();

        public IEnumerable<Member> RunCampaign(int campaignId)
        {
            return searchRepo.RunCampaign(campaignId);
        }

        public IEnumerable<SearchCriteria> GetEmptySearchCriteria()
        {
            return searchRepo.GetEmptySearchCriteria();
        }
    }
}