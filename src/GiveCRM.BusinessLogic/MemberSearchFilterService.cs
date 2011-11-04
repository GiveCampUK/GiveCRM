using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.BusinessLogic
{
    internal class MemberSearchFilterService : IMemberSearchFilterService
    {
        private readonly MemberSearchFilters memberSearchFilterRepo = new MemberSearchFilters();

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
}