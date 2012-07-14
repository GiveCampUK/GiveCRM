namespace GiveCRM.BusinessLogic
{
    using System.Collections.Generic;
    using GiveCRM.Models;

    public interface IDonationsService
    {
        IEnumerable<Donation> GetTopDonations(int count);
        IEnumerable<Donation> GetLatestDonations(int count);
        void QuickDonation(Donation donation);
    }
}