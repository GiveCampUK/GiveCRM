namespace GiveCRM.BusinessLogic
{
    using System.Collections.Generic;
    using GiveCRM.Models;

    public interface IFacetsService
    {
        IEnumerable<Facet> All();
        Facet Get(int id);
        void Insert(Facet facet);
        void Update(Facet facet);
    }
}