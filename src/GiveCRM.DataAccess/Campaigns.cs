using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveCRM.Models;
using Simple.Data;

namespace GiveCRM.DataAccess
{
    public class Campaigns
    {
        private readonly dynamic _db = Database.OpenNamedConnection("GiveCRM");

        public Campaign Get(int id)
        {
            return _db.Campaigns.FindById(id);
        }

        public IEnumerable<Campaign> All()
        {
            return _db.Campaigns.All().OrderByRunOnDescending().Cast<Campaign>();
        }

        public IEnumerable<Campaign> AllOpen()
        {
            return _db.Campaigns.FindAllByIsClosed('N').OrderByRunOnDescending().Cast<Campaign>();
        }

        public IEnumerable<Campaign> AllClosed()
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
