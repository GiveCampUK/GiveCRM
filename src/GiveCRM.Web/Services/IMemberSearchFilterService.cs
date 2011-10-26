using System.Collections.Generic;
using GiveCRM.DataAccess;
using GiveCRM.Models;

namespace GiveCRM.Web.Services
{
    public class MemberSearchFilterService : IMemberSearchFilterService
    {
        private MemberSearchFilters memberSearchFilterRepo = new MemberSearchFilters();

        public IEnumerable<MemberSearchFilter> ForCampaign(int id)
        {
            return memberSearchFilterRepo.ForCampaign(id);
        }

        public void Insert(MemberSearchFilter memberSearchFilter)
        {
            memberSearchFilterRepo.Insert(memberSearchFilter);
        }

        public void Delete(int id)
        {
            memberSearchFilterRepo.Delete(id);
        }

    }


    public interface IMemberSearchFilterService
    {
        IEnumerable<MemberSearchFilter> ForCampaign(int id);
        void Insert(MemberSearchFilter memberSearchFilter);
        void Delete(int id);
    }
}