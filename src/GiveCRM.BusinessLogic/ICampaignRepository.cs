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
    }
}