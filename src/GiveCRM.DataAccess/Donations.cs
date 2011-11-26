namespace GiveCRM.DataAccess
{
    using System.Collections.Generic;
	using GiveCRM.BusinessLogic;
    using GiveCRM.Models;
    using Simple.Data;

    public class Donations : IDonationRepository
    {
        private readonly dynamic db = Database.OpenNamedConnection("GiveCRM");

        public Donation GetById(int id)
        {
            return db.Donations.FindById(id);
        }

        public void Update(Donation item)
        {
            db.Donations.UpdateById(item);
        }

        public IEnumerable<Donation> GetAll()
        {
            return db.Donations.All().Cast<Donation>();
        }

        public IEnumerable<Donation> GetByMemberId(int memberId)
        {
            return db.Donations.FindByMemberId(memberId).Cast<Donation>();
        }

        public IEnumerable<Donation> GetByCampaignId(int campaignId)
        {
            return db.Donations.FindByCampaignId(campaignId).Cast<Donation>();
        }

        public Donation Insert(Donation donation)
        {
            return db.Donations.Insert(donation);
        }

        /// <summary>
        /// Deletes the donation identified by the specified identifier.  
        /// </summary>
        /// <param name="id">The identifier of the donation to delete.</param>
        public void DeleteById(int id)
        {
            db.Donations.DeleteById(id);
        }
    }
}
