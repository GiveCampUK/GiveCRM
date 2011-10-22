using System.Collections.Generic;

using GiveCRM.Models;

namespace GiveCRM.Web.Models.Search
{
    public class SearchService
    {
        public IEnumerable<SearchCriteria> GetSearchCriteria()
        {
            return null;
        }

        public IEnumerable<Member> SearchForMembers(IEnumerable<SearchCriteria> criteria)
        {
            return null;
        }
    }
}