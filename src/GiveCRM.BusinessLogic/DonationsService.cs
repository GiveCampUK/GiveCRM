using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.BusinessLogic
{
    internal class DonationsService : IDonationsService
    {
        private Donations _donationsDb = new Donations();

        public  IEnumerable<Donation> GetTopDonations()
        {
            var donations = _donationsDb.All().OrderByDescending(d => d.Amount).Take(5);
            return donations;
        }

        public IEnumerable<Donation> GetLatestDonations()
        {
            var donations = _donationsDb.All().OrderByDescending(d => d.Date).Take(5);
            return donations;
        }

        public void QuickDonation(Donation donation)
        {
            _donationsDb.Insert(donation);
        }
    }


}