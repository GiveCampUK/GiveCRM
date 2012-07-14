namespace GiveCRM.BusinessLogic
{
    using System.Collections.Generic;
    using GiveCRM.Models;

    public interface IMemberSearchFilterService
    {
        IEnumerable<MemberSearchFilter> ForCampaign(int id);
        void Insert(MemberSearchFilter memberSearchFilter);
        void Delete(int id);
    }
}