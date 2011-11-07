using System.Collections.Generic;
using GiveCRM.Models;
using Simple.Data;

namespace GiveCRM.DataAccess
{
    public class MemberSearchFilters
    {
        private readonly dynamic db = Database.OpenNamedConnection("GiveCRM");

        public IEnumerable<MemberSearchFilter> ForCampaign(int campaignId)
        {
            return db.MemberSearchFilters.FindAllByCampaignId(campaignId).Cast<MemberSearchFilter>();
        }

        public void Insert(MemberSearchFilter memberSearchFilter)
        {
            db.MemberSearchFilters.Insert(memberSearchFilter);
        }

        public void Delete(int id)
        {
            db.MemberSearchFilters.DeleteById(id);
        }
    }
}
