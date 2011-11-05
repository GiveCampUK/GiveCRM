using System.Collections.Generic;
using GiveCRM.BusinessLogic;
using GiveCRM.Models;
using Simple.Data;

namespace GiveCRM.DataAccess
{
    public class Campaigns : ICampaignRepository
    {
        private readonly dynamic _db = Database.OpenNamedConnection("GiveCRM");

        public Campaign GetById(int id)
        {
            return _db.Campaigns.FindById(id);
        }

        public IEnumerable<Campaign> GetAll()
        {
            return _db.Campaigns.All().OrderByRunOnDescending().Cast<Campaign>();
        }

        public IEnumerable<Campaign> GetAllOpen()
        {
            return _db.Campaigns.FindAllByIsClosed('N').OrderByRunOnDescending().Cast<Campaign>();
        }

        public IEnumerable<Campaign> GetAllClosed()
        {
            return _db.Campaigns.FindAllByIsClosed('Y').OrderByRunOnDescending().Cast<Campaign>();
        }

        public Campaign Insert(Campaign campaign)
        {
            return _db.Campaigns.Insert(campaign);
        }

        public void Update(Campaign campaign)
        {
            _db.Campaigns.UpdateById(campaign);
        }
    }
}
