using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Deletes the item of type <typeparamref name="T"/> identified by the specified identifier.  
        /// </summary>
        /// <param name="id">The identifier of the item to delete.</param>
        public void DeleteById(int id)
        {
            _db.Campaigns.Delete(id);
        }

        public void Update(Campaign campaign)
        {
            _db.Campaigns.UpdateById(campaign);
        }

        public void Commit(int campaignId, IEnumerable<Member> campaignMembers)
        {
            var results = campaignMembers.Select(member => new { CampaignId = campaignId, MemberId = member.Id });

            using (var transaction = _db.BeginTransaction())
            {
                try
                {
                    transaction.Campaign.UpdateById(Id: campaignId, runOn: DateTime.Today);
                    transaction.CampaignRuns.Insert(results);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

        }
    }
}
