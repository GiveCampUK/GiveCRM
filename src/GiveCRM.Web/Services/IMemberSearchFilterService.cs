using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.Web.Services
{
    public interface IMemberSearchFilterService
    {
        IEnumerable<MemberSearchFilter> ForCampaign(int id);
        void Insert(MemberSearchFilter memberSearchFilter);
        void Delete(int id);
    }
}