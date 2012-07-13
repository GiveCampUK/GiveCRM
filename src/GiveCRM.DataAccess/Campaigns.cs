using System;
using System.Collections.Generic;
using System.Linq;
using GiveCRM.BusinessLogic;
using GiveCRM.Models;

namespace GiveCRM.DataAccess
{
    using GiveCRM.Infrastructure;

    public class Campaigns : ICampaignRepository
    {
        private readonly IDatabaseProvider databaseProvider;

        public Campaigns(IDatabaseProvider databaseProvider)
        {
            this.databaseProvider = databaseProvider;
        }

        public Campaign GetById(int id)
        {
            return databaseProvider.GetDatabase().Campaigns.FindById(id);
        }

        public IEnumerable<Campaign> GetAll()
        {
            return databaseProvider.GetDatabase().Campaigns.All().OrderByRunOnDescending().Cast<Campaign>();
        }

        public IEnumerable<Campaign> GetAllOpen()
        {
            return databaseProvider.GetDatabase().Campaigns.FindAllByIsClosed('N').OrderByRunOnDescending().Cast<Campaign>();
        }

        public IEnumerable<Campaign> GetAllClosed()
        {
            return databaseProvider.GetDatabase().Campaigns.FindAllByIsClosed('Y').OrderByRunOnDescending().Cast<Campaign>();
        }

        public Campaign Insert(Campaign campaign)
        {
            return databaseProvider.GetDatabase().Campaigns.Insert(campaign);
        }

        /// <summary>
        /// Deletes the campaign identified by the specified identifier.  
        /// </summary>
        /// <param name="id">The identifier of the campaign to delete.</param>
        public void DeleteById(int id)
        {
            databaseProvider.GetDatabase().Campaigns.Delete(id);
        }

        public void Update(Campaign campaign)
        {
            databaseProvider.GetDatabase().Campaigns.UpdateById(campaign);
        }

        public void Commit(int campaignId, IEnumerable<Member> campaignMembers)
        {
            var results = campaignMembers.Select(member => new { CampaignId = campaignId, MemberId = member.Id });

            using (var transaction = TransactionScopeFactory.Create())
            {
                var database = databaseProvider.GetDatabase();

                database.Campaign.UpdateById(Id: campaignId, runOn: DateTime.Today);
                database.CampaignRuns.Insert(results);

                transaction.Complete();
            }
        }
    }
}
