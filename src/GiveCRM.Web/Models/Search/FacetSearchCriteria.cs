using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GiveCRM.Models;
using GiveCRM.DataAccess;

namespace GiveCRM.Web.Models.Search
{
    public class FacetSearchCriteria : SearchCriteria
    {
        public static IEnumerable<SearchCriteria> GetEmptyCriteria()
        {
            return null;
        }

        public override bool IsMatch(Member m)
        {
            switch (this.InternalName)
            {
                default: return false;
            }
        }
    }
}