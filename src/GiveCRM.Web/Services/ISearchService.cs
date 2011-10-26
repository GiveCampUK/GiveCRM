using System.Collections.Generic;
using GiveCRM.Models;
using GiveCRM.Models.Search;

namespace GiveCRM.Web.Services
{
    public interface ISearchService
    {
        IEnumerable<Member> RunCampaign(int campaignId);
        IEnumerable<SearchCriteria> GetEmptySearchCriteria();
    }
}