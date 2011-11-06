using System.Collections.Generic;
using GiveCRM.BusinessLogic;
using GiveCRM.Models;
using Simple.Data;

namespace GiveCRM.DataAccess
{
    public class MemberSearchFilters : IMemberSearchFilterRepository
    {
        private readonly dynamic _db = Database.OpenNamedConnection("GiveCRM");

        public IEnumerable<MemberSearchFilter> ForCampaign(int campaignId)
        {
            return _db.MemberSearchFilters.FindAllByCampaignId(campaignId).Cast<MemberSearchFilter>();
        }

        public IEnumerable<MemberSearchFilter> GetAll()
        {
            return _db.MemberSearchFilters.All();
        }

        public MemberSearchFilter GetById(int id)
        {
            return _db.MemberSearchFilters.FindById(id);
        }

        public void Update(MemberSearchFilter item)
        {
            _db.MemberSearchFilters.UpdateById(item);
        }

        public MemberSearchFilter Insert(MemberSearchFilter memberSearchFilter)
        {
            return _db.MemberSearchFilters.Insert(memberSearchFilter);
        }
        
        public void DeleteById(int id)
        {
            _db.MemberSearchFilters.DeleteById(id);
        }

        /// <summary>
        /// Gets a list of all the <see cref="MemberSearchFilter"/>s associated with the specified <see cref="Campaign"/>.
        /// </summary>
        /// <param name="id">The campaign identifier.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="MemberSearchFilter"/> used in the 
        /// specified <see cref="Campaign"/>.</returns>
        public IEnumerable<MemberSearchFilter> GetByCampaignId(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
