using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.BusinessLogic
{
    /// <summary>
    /// Exposes basic CRUD functionality for <see cref="Member"/>s (see <see cref="IRepository{T}"/>), plus
    /// search behaviours and <see cref="Member"/> behaviours associated with <see cref="Campaign"/>s.
    /// </summary>
    public interface IMemberRepository : IRepository<Member>
    {
        /// <summary>
        /// Searches for <see cref="Member"/>s matching the provided search criteria. The criteria are 
        /// combined using a logical AND.
        /// </summary>
        /// <param name="name">The <see cref="Member"/>'s name.</param>
        /// <param name="postcode">The <see cref="Member"/>'s postcode.</param>
        /// <param name="reference">The <see cref="Member"/>'s reference.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all <see cref="Member"/>s matching the
        /// provided search criteria.</returns>
        IEnumerable<Member> Search(string name, string postcode, string reference);

        /// <summary>
        /// Returns a list of <see cref="Member" />s associated with the campaign run
        /// identified by the provided identifier.
        /// </summary>
        /// <param name="campaignId">The campaign id.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Member"/>s associated with the
        /// specified campaign run.</returns>
        IEnumerable<Member> GetByCampaignId(int campaignId);
    }
}