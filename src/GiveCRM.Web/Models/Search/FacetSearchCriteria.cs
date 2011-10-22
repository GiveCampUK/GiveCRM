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
        private static Facets facets = new Facets();

        public static IEnumerable<SearchCriteria> GetEmptyCriteria()
        {
            foreach (Facet f in facets.All())
            {
                if (f.Type == FacetType.FreeText)
                {
                    yield return new FacetSearchCriteria { InternalName="freeTextFacet_"+f.Id, DisplayName=f.Name, Type=SearchFieldType.String };
                }
            }
        }

        public override bool IsMatch(Member m)
        {
            if (this.InternalName.StartsWith("freeTextFacet"))
            {
                int facetId = int.Parse(this.InternalName.Replace("freeTextFacet_", ""));
                return Evaluate(facets.Get(facetId));
            }

            return false;
        }
    }
}