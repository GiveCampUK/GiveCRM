using System.Collections.Generic;
using GiveCRM.Models;
using GiveCRM.Models.Search;

namespace GiveCRM.BusinessLogic
{
    public class SearchService : ISearchService
    {
        private readonly Search _repository = new Search();

        public IEnumerable<Member> RunCampaign(int campaignId)
        {
            return _repository.RunCampaign(campaignId);
        }

        public IEnumerable<SearchCriteria> GetEmptySearchCriteria()
        {
            return _repository.GetEmptySearchCriteria();
        }
    }
}