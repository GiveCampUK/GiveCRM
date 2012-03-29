using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.DataAccess.Test
{
    public static class FacetSetUpHelper
    {
        public static Facet CreateListFacet()
        {
            var facets = new Facets(new DatabaseProvider());
            var facet = new Facet
                            {
                                Type = FacetType.List,
                                Name = "ListTest",
                                Values = new List<FacetValue>
                                             {
                                                 new FacetValue {Value = "One"},
                                                 new FacetValue {Value = "Two"},
                                             }
                            };
            var record = facets.Insert(facet);
            facet = record;
            return facet;
        }

        public static Facet CreateFreeTextFacet()
        {
            var facets = new Facets(new DatabaseProvider());
            var facet = new Facet
                            {
                                Type = FacetType.FreeText,
                                Name = "FreeTextTest"
                            };
            var record = facets.Insert(facet);
            facet = record;
            return facet;
        }
    }
}