using System.Collections.Generic;
using GiveCRM.BusinessLogic;
using GiveCRM.Models;
using Simple.Data;

namespace GiveCRM.DataAccess
{
    public class Donations : IDonationRepository
    {
        private readonly IDatabaseProvider databaseProvider;

        public Donations(IDatabaseProvider databaseProvider)
        {
            this.databaseProvider = databaseProvider;
        }

        public Donation GetById(int id)
        {
            return databaseProvider.GetDatabase().Donations.FindById(id);
        }

        public void Update(Donation item)
        {
            databaseProvider.GetDatabase().Donations.UpdateById(item);
        }

        public IEnumerable<Donation> GetAll()
        {
            return databaseProvider.GetDatabase().Donations.All().Cast<Donation>();
        }

        public IEnumerable<Donation> GetByMemberId(int memberId)
        {
            return databaseProvider.GetDatabase().Donations.FindByMemberId(memberId).Cast<Donation>();
        }

        public IEnumerable<Donation> GetByCampaignId(int campaignId)
        {
            return databaseProvider.GetDatabase().Donations.FindByCampaignId(campaignId).Cast<Donation>();
        }

        public Donation Insert(Donation donation)
        {
            return databaseProvider.GetDatabase().Donations.Insert(donation);
        }

        /// <summary>
        /// Deletes the donation identified by the specified identifier.  
        /// </summary>
        /// <param name="id">The identifier of the donation to delete.</param>
        public void DeleteById(int id)
        {
            databaseProvider.GetDatabase().Donations.DeleteById(id);
        }
    }
}
