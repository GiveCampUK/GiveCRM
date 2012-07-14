namespace GiveCRM.BusinessLogic
{
    using System.Collections.Generic;
    using GiveCRM.Models.Search;

    public interface ISearchService
    {
        IEnumerable<SearchCriteria> GetEmptySearchCriteria();
    }
}