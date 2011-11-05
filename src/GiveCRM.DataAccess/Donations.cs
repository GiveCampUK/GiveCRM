using System.Collections.Generic;
using GiveCRM.BusinessLogic;
using GiveCRM.Models;
using Simple.Data;

namespace GiveCRM.DataAccess
{
    public class Donations : IDonationRepository
    {
        private readonly dynamic _db = Database.OpenNamedConnection("GiveCRM");

        public Donation GetById(int id)
        {
            return _db.Donations.FindById(id);
        }

        public void Update(Donation item)
        {
            _db.Donations.UpdateById(item);
        }

        public IEnumerable<Donation> GetAll()
        {
            return _db.Donations.All().Cast<Donation>();
        }

        public IEnumerable<Donation> GetByMemberId(int memberId)
        {
            return _db.Donations.FindByMemberId(memberId).Cast<Donation>();
        }

        public IEnumerable<Donation> GetByCampaignId(int campaignId)
        {
            return _db.Donations.FindByCampaignId(campaignId).Cast<Donation>();
        }

        public Donation Insert(Donation donation)
        {
            return _db.Donations.Insert(donation);
        }
    }
}
