using System.Collections.Generic;
using GiveCRM.BusinessLogic;
using GiveCRM.Models;
using Simple.Data;

namespace GiveCRM.DataAccess
{
    public class MemberSearchFilters : IMemberSearchFilterRepository
    {
        private readonly IDatabaseProvider databaseProvider;

        public MemberSearchFilters(IDatabaseProvider databaseProvider)
        {
            this.databaseProvider = databaseProvider;
        }

        public IEnumerable<MemberSearchFilter> GetByCampaignId(int campaignId)
        {
            return databaseProvider.GetDatabase().MemberSearchFilters.FindAllByCampaignId(campaignId).Cast<MemberSearchFilter>();
        }

        public IEnumerable<MemberSearchFilter> GetAll()
        {
            return databaseProvider.GetDatabase().MemberSearchFilters.All();
        }

        public MemberSearchFilter GetById(int id)
        {
            return databaseProvider.GetDatabase().MemberSearchFilters.FindById(id);
        }

        public void Update(MemberSearchFilter item)
        {
            databaseProvider.GetDatabase().MemberSearchFilters.UpdateById(item);
        }

        public MemberSearchFilter Insert(MemberSearchFilter memberSearchFilter)
        {
            return databaseProvider.GetDatabase().MemberSearchFilters.Insert(memberSearchFilter);
        }
        
        public void DeleteById(int id)
        {
            databaseProvider.GetDatabase().MemberSearchFilters.DeleteById(id);
        }
    }
}
