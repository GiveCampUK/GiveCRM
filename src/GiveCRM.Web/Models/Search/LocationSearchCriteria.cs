using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GiveCRM.Models;
using GiveCRM.DataAccess;

namespace GiveCRM.Web.Models.Search
{
    public class LocationSearchCriteria : SearchCriteria
    {
        public static IEnumerable<SearchCriteria> GetEmptyCriteria()
        {
            yield return new LocationSearchCriteria { InternalName = "city", DisplayName = "City", Type = SearchFieldType.String };
            yield return new LocationSearchCriteria { InternalName = "region", DisplayName = "Region", Type = SearchFieldType.String };
            yield return new LocationSearchCriteria { InternalName = "postalCode", DisplayName = "Postal code", Type = SearchFieldType.String };
        }

        public override bool IsMatch(Member m)
        {
            switch (this.InternalName)
            {
                case "city": return Evaluate(m.City);
                case "region": return Evaluate(m.Region);
                case "postalCode": return Evaluate(m.PostalCode);
                default: return false;
            }
        }
    }
}