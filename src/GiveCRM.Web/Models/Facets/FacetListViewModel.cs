using System.Collections.Generic;

namespace GiveCRM.Web.Models.Facets
{
    using GiveCRM.Models;

    public class FacetListViewModel
    {
        public IEnumerable<Facet> Facets { get; set; }
    }
}