using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.Web.Services
{
    public interface ICampaignService
    {
        IEnumerable<Campaign> AllOpen();
        IEnumerable<Campaign> AllClosed();
        Campaign Get(int id);
        Campaign Insert(Campaign campaign);
        void Update(Campaign campaign);
    }
}