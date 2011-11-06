using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.BusinessLogic
{
    /// <summary>
    /// Exposes basic CRUD functionality for <see cref="Campaign"/>s (see <see cref="IRepository{T}"/>), plus
    /// some <see cref="Campaign"/>-specific functionality.
    /// </summary>
    public interface ICampaignRepository : IRepository<Campaign>
    {
        /// <summary>
        /// Gets a list of all the open campaigns.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Campaign"/>s.</returns>
        IEnumerable<Campaign> GetAllOpen();

        /// <summary>
        /// Gets a list of all the closed campaigns.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Campaign"/>s.</returns>
        IEnumerable<Campaign> GetAllClosed();

        /// <summary>
        /// Commits the specified campaign id.  Once a campaign has been committed, its search criteria cannot be modified.
        /// </summary>
        /// <param name="campaignId">The campaign id.</param>
        /// <param name="campaignMembers">The campaign members.</param>
        void Commit(int campaignId, IEnumerable<Member> campaignMembers);
    }
}