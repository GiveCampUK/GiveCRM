namespace GiveCRM.BusinessLogic
{
    using System.Collections.Generic;
    using GiveCRM.Models.Search;

    public interface ISearchQueryService
    {
        IEnumerable<T> CompileQuery<T>(IEnumerable<SearchCriteria> criteriaList);
    }
}