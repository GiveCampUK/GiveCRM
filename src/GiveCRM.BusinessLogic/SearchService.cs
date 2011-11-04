using System.Collections.Generic;
using GiveCRM.Models;
using GiveCRM.Models.Search;

namespace GiveCRM.BusinessLogic
{
    public class SearchService: ISearchService
    {
        private Search searchRepo = new Search();

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