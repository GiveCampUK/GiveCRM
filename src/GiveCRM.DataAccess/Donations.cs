using System.Collections.Generic;

using GiveCRM.Models;
using Simple.Data;

namespace GiveCRM.DataAccess
{
    public class Donations
    {
        private readonly dynamic _db = Database.OpenNamedConnection("GiveCRM");

        public Donation Get(int id)
        {
            return _db.Donations.FindById(id);
        }

        public IEnumerable<Donation> All()
        {
            return _db.Donations.All().Cast<Donation>();
        }

        public IEnumerable<Donation> ByMember(int memberId)
        {
            return _db.Donations.FindByMemberId(memberId).Cast<Donation>();
        }

        public IEnumerable<Donation> ByCampaign(int campaignId)
        {
            return _db.Donations.FindByCampaignId(campaignId).Cast<Donation>();
        }

        public Donation Insert(Donation donation)
        {
            return _db.Donations.Insert(donation);
        }
    }
}
