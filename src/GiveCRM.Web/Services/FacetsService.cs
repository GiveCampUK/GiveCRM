using System.Collections.Generic;
using GiveCRM.DataAccess;
using GiveCRM.Models;

namespace GiveCRM.Web.Services
{
    public class FacetsService: IFacetsService
    {
        private readonly Facets _facetsDb = new Facets();

        public IEnumerable<Facet> All()
        {
            var facets = _facetsDb.All();
            return facets;
        }

        public Facet Get(int id)
        {
            var facet = _facetsDb.Get(id);
            return facet;
        }

        public void Insert(Facet facet)
        {
            _facetsDb.Insert(facet);             
        }

        public void Update(Facet facet)
        {
            _facetsDb.Update(facet);
        }
    }

    
}