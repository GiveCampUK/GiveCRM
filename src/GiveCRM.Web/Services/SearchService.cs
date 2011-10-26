using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GiveCRM.DataAccess;
using GiveCRM.Models;
using GiveCRM.Models.Search;

namespace GiveCRM.Web.Services
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