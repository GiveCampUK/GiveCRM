namespace GiveCRM.DataAccess
{
    using System.Collections.Generic;

    using GiveCRM.Models;
    using Simple.Data;

    public class Campaigns
    {
        private readonly dynamic db = Database.OpenNamedConnection("GiveCRM");

        public Campaign Get(int id)
        {
            return db.Campaigns.FindById(id);
        }

        public IEnumerable<Campaign> All()
        {
            return db.Campaigns.All().OrderByRunOnDescending().Cast<Campaign>();
        }

        public IEnumerable<Campaign> AllOpen()
        {
            return db.Campaigns.FindAllByIsClosed('N').OrderByRunOnDescending().Cast<Campaign>();
        }

        public IEnumerable<Campaign> AllClosed()
        {
            return db.Campaigns.FindAllByIsClosed('Y').OrderByRunOnDescending().Cast<Campaign>();
        }

        public Campaign Insert(Campaign campaign)
        {
            return db.Campaigns.Insert(campaign);
        }

        public void Update(Campaign campaign)
        {
            db.Campaigns.UpdateById(campaign);
        }
    }
}
