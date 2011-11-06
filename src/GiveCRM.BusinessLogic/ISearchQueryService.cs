using System.Collections.Generic;
using GiveCRM.Models.Search;

namespace GiveCRM.BusinessLogic
{
    public interface ISearchQueryService
    {
        dynamic CompileQuery(List<SearchCriteria> criteriaList);
    }
}