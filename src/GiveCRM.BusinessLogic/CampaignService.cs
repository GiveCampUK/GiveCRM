using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.BusinessLogic
{
    internal class CampaignService: ICampaignService
    {
        private readonly Campaigns _campaignsDb = new Campaigns();

        public IEnumerable<Campaign> AllOpen()
        {
            return _campaignsDb.AllOpen();
        }

        public IEnumerable<Campaign> AllClosed()
        {
            return _campaignsDb.AllClosed();
        }

        public Campaign Get(int id)
        {
            return _campaignsDb.Get(id);
        }

        public Campaign Insert(Campaign campaign)
        {
            return _campaignsDb.Insert(campaign);
        }

        public void Update(Campaign campaign)
        {
            _campaignsDb.Update(campaign);
        }

    }
}