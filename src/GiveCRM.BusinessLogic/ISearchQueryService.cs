using System.Collections;
using System.Collections.Generic;
using GiveCRM.Models.Search;

namespace GiveCRM.BusinessLogic
{
    public interface ISearchQueryService
    {
        IEnumerable CompileQuery(IEnumerable<SearchCriteria> criteriaList);
    }
}