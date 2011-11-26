using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.BusinessLogic
{
    public interface IFacetRepository : IRepository<Facet>
    {
        IEnumerable<Facet> GetAllFreeText();
    }
}
