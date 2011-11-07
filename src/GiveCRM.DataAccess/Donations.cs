using System.Collections.Generic;

using GiveCRM.Models;
using Simple.Data;

namespace GiveCRM.DataAccess
{
    public class Donations
    {
        private readonly dynamic db = Database.OpenNamedConnection("GiveCRM");

        public Donation Get(int id)
        {
            return db.Donations.FindById(id);
        }

        public IEnumerable<Donation> All()
        {
            return db.Donations.All().Cast<Donation>();
        }

        public IEnumerable<Donation> ByMember(int memberId)
        {
            return db.Donations.FindByMemberId(memberId).Cast<Donation>();
        }

        public IEnumerable<Donation> ByCampaign(int campaignId)
        {
            return db.Donations.FindByCampaignId(campaignId).Cast<Donation>();
        }

        public Donation Insert(Donation donation)
        {
            return db.Donations.Insert(donation);
        }
    }
}
