using System.Collections.Generic;
using GiveCRM.DataAccess;
using GiveCRM.Models;

namespace GiveCRM.Web.Services
{
    public class FacetsService: IFacetsService
    {
        private readonly Facets facetsDb = new Facets();

        public IEnumerable<Facet> All()
        {
            var facets = facetsDb.All();
            return facets;
        }

        public Facet Get(int id)
        {
            var facet = facetsDb.Get(id);
            return facet;
        }

        public void Insert(Facet facet)
        {
            facetsDb.Insert(facet);             
        }

        public void Update(Facet facet)
        {
            facetsDb.Update(facet);
        }
    }

    
}