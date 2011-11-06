using System.Collections.Generic;
using GiveCRM.Models.Search;

namespace GiveCRM.BusinessLogic
{
    public interface ISearchService
    {
        IEnumerable<SearchCriteria> GetEmptySearchCriteria();
    }
}