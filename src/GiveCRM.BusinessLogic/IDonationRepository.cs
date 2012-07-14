namespace GiveCRM.BusinessLogic
{
    using System.Collections.Generic;
    using GiveCRM.Models;

    /// <summary>
    /// Exposes basic CRUD functionality for <see cref="Donation"/>s (see <see cref="IRepository{T}"/>), plus
    /// donation-specific behaviours.
    /// </summary>
    public interface IDonationRepository : IRepository<Donation>
    {
        /// <summary>
        /// Gets a list of all the donations made by the specified <see cref="Member"/>.
        /// </summary>
        /// <param name="memberId">The identifier of the <see cref="Member"/>.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Donation"/>s made by the
        /// specified <see cref="Member"/>.</returns>
        IEnumerable<Donation> GetByMemberId(int memberId);

        /// <summary>
        /// Gets a list of all the donations recorded under the specified <see cref="Campaign"/>.
        /// </summary>
        /// <param name="campaignId">The identifier of the <see cref="Campaign"/>.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Donation"/>s recorded under the
        /// specified <see cref="Campaign"/>.</returns>
        IEnumerable<Donation> GetByCampaignId(int campaignId);
    }
}