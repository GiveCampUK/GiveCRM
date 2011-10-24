using System.Collections.Generic;
using GiveCRM.DataAccess;
using GiveCRM.Models;

namespace GiveCRM.Web.Services
{
    public class CampaignService: ICampaignService
    {
        private Campaigns _campaignsDb = new Campaigns();

        public IEnumerable<Campaign> AllOpen()
        {
            return _campaignsDb.AllOpen();
        }
    }

    public interface ICampaignService
    {
        IEnumerable<Campaign> AllOpen();
    }
}