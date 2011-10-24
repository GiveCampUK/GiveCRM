using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.Web.Services
{
    public interface IFacetsService
    {
        IEnumerable<Facet> All();
        Facet Get(int id);
        void Insert(Facet facet);
        void Update(Facet facet);
    }
}