namespace GiveCRM.DataAccess.Test
{
    using System.Collections.Generic;
    using GiveCRM.Models;

    public static class FacetSetUpHelper
    {
        public static Facet CreateListFacet(IDatabaseProvider databaseProvider)
        {
            var facets = new Facets(databaseProvider);
            var facet = new Facet
                            {
                                Type = FacetType.List,
                                Name = "ListTest",
                                Values = new List<FacetValue>
                                             {
                                                 new FacetValue { Value = "One" },
                                                 new FacetValue { Value = "Two" },
                                             }
                            };
            var record = facets.Insert(facet);
            facet = record;
            return facet;
        }

        public static Facet CreateFreeTextFacet(IDatabaseProvider databaseProvider)
        {
            var facets = new Facets(databaseProvider);
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