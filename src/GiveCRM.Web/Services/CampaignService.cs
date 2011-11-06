using System.Collections.Generic;
using GiveCRM.DataAccess;
using GiveCRM.Models;

namespace GiveCRM.Web.Services
{
    public class CampaignService: ICampaignService
    {
        private readonly Campaigns campaignsDb = new Campaigns();

        public IEnumerable<Campaign> AllOpen()
        {
            return campaignsDb.AllOpen();
        }

        public IEnumerable<Campaign> AllClosed()
        {
            return campaignsDb.AllClosed();
        }

        public Campaign Get(int id)
        {
            return campaignsDb.Get(id);
        }

        public Campaign Insert(Campaign campaign)
        {
            return campaignsDb.Insert(campaign);
        }

        public void Update(Campaign campaign)
        {
            campaignsDb.Update(campaign);
        }

    }
}