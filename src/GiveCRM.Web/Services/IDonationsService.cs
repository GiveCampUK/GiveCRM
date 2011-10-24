using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.Web.Services
{
    public interface IDonationsService
    {
        IEnumerable<Donation> GetTopDonations();
        IEnumerable<Donation> GetLatestDonations();
        void QuickDonation(Donation donation);
    }
}