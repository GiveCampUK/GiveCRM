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
        private readonly dynamic db = Database.OpenNamedConnection("GiveCRM");

        public Campaign GetById(int id)
        {
            return db.Campaigns.FindById(id);
        }

        public IEnumerable<Campaign> GetAll()
        {
            return db.Campaigns.All().OrderByRunOnDescending().Cast<Campaign>();
        }

        public IEnumerable<Campaign> GetAllOpen()
        {
            return db.Campaigns.FindAllByIsClosed('N').OrderByRunOnDescending().Cast<Campaign>();
        }

        public IEnumerable<Campaign> GetAllClosed()
        {
            return db.Campaigns.FindAllByIsClosed('Y').OrderByRunOnDescending().Cast<Campaign>();
        }

        public Campaign Insert(Campaign campaign)
        {
            return db.Campaigns.Insert(campaign);
        }

        /// <summary>
        /// Deletes the campaign identified by the specified identifier.  
        /// </summary>
        /// <param name="id">The identifier of the campaign to delete.</param>
        public void DeleteById(int id)
        {
            db.Campaigns.Delete(id);
        }

        public void Update(Campaign campaign)
        {
            db.Campaigns.UpdateById(campaign);
        }

        public void Commit(int campaignId, IEnumerable<Member> campaignMembers)
        {
            var results = campaignMembers.Select(member => new { CampaignId = campaignId, MemberId = member.Id });

            using (var transaction = db.BeginTransaction())
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
