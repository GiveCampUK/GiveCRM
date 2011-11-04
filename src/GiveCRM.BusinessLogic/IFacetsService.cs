using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.BusinessLogic
{
    public interface IFacetsService
    {
        IEnumerable<Facet> All();
        Facet Get(int id);
        void Insert(Facet facet);
        void Update(Facet facet);
    }
}