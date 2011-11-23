using System.Collections.Generic;
using System.Linq;
using GiveCRM.DataAccess;
using GiveCRM.Models;

namespace GiveCRM.Web.Services
{
    public class DonationsService : IDonationsService
    {
        private readonly Donations donationsDb = new Donations();

        public  IEnumerable<Donation> GetTopDonations()
        {
            var donations = donationsDb.All().OrderByDescending(d => d.Amount).Take(5);
            return donations;
        }

        public IEnumerable<Donation> GetLatestDonations()
        {
            var donations = donationsDb.All().OrderByDescending(d => d.Date).Take(5);
            return donations;
        }

        public void QuickDonation(Donation donation)
        {
            donationsDb.Insert(donation);
        }
    }
}