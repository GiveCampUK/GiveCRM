using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.BusinessLogic
{
    public interface ICampaignService
    {
        IEnumerable<Campaign> GetAllOpen();
        IEnumerable<Campaign> GetAllClosed();
        Campaign Get(int id);
        Campaign Insert(Campaign campaign);
        void Update(Campaign campaign);
        void Commit(int campaignId);
    }
}