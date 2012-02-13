using System.Collections.Generic;
using GiveCRM.BusinessLogic;
using GiveCRM.Models;
using Simple.Data;

namespace GiveCRM.DataAccess
{


    public class MemberSearchFilters : IMemberSearchFilterRepository
    {
        private readonly dynamic db = Database.OpenNamedConnection("GiveCRM");

        public IEnumerable<MemberSearchFilter> GetByCampaignId(int campaignId)
        {
            return db.MemberSearchFilters.FindAllByCampaignId(campaignId).Cast<MemberSearchFilter>();
        }

        public IEnumerable<MemberSearchFilter> GetAll()
        {
            return db.MemberSearchFilters.All();
        }

        public MemberSearchFilter GetById(int id)
        {
            return db.MemberSearchFilters.FindById(id);
        }

        public void Update(MemberSearchFilter item)
        {
            db.MemberSearchFilters.UpdateById(item);
        }

        public MemberSearchFilter Insert(MemberSearchFilter memberSearchFilter)
        {
            return db.MemberSearchFilters.Insert(memberSearchFilter);
        }
        
        public void DeleteById(int id)
        {
            db.MemberSearchFilters.DeleteById(id);
        }
    }
}
