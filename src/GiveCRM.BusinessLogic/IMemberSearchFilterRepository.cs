using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.BusinessLogic
{
    /// <summary>
    /// Exposes basic CRUD functionality for <see cref="MemberSearchFilter"/>s (see <see cref="IRepository{T}"/>), plus
    /// behaviours specific to member search filters.
    /// </summary>
    public interface IMemberSearchFilterRepository : IRepository<MemberSearchFilter>
    {
        /// <summary>
        /// Gets a list of all the <see cref="MemberSearchFilter"/>s associated with the specified <see cref="Campaign"/>.
        /// </summary>
        /// <param name="id">The campaign identifier.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="MemberSearchFilter"/> used in the 
        /// specified <see cref="Campaign"/>.</returns>
        IEnumerable<MemberSearchFilter> GetByCampaignId(int id);
    }
}