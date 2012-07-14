namespace GiveCRM.BusinessLogic
{
    using System.Collections.Generic;
    using GiveCRM.Models;

    public interface IFacetRepository : IRepository<Facet>
    {
        IEnumerable<Facet> GetAllFreeText();
    }
}
