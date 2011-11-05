using System.Collections.Generic;
using GiveCRM.BusinessLogic;
using GiveCRM.Models;
using Simple.Data;

namespace GiveCRM.DataAccess
{
    public class MemberSearchFilters : IRepository<MemberSearchFilter>
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

        public void Delete(int id)
        {
            _db.MemberSearchFilters.DeleteById(id);
        }
    }
}
