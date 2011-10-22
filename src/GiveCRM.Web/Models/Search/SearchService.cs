using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GiveCRM.DataAccess;
using GiveCRM.Models;

namespace GiveCRM.Web.Models.Search
{
    public class SearchService
    {
        public IEnumerable<SearchCriteria> GetSearchCriteria()
        {
            yield return new SearchCriteria { Field="Donation amount", DatabaseTableName="Donation", DatabaseColumnName="Amount", Type=SearchFieldType.Number };
            yield return new SearchCriteria { Field="Donation date", DatabaseTableName="Donation", DatabaseColumnName="Date", Type=SearchFieldType.Date };
            yield return new SearchCriteria { Field="Contributed to campaign", DatabaseTableName="Campaign", DatabaseColumnName="Name", Type=SearchFieldType.Text };
        }

        public IEnumerable<Member> SearchForMembers(IEnumerable<SearchCriteria> criteria)
        {
            return null;
        }
    }
}