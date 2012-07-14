namespace GiveCRM.Web.Models.Donation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using GiveCRM.Models;

    public class QuickDonateViewModel
    {
        public int DonorId { get; private set; }
        public IEnumerable<SelectListItem> Campaigns { get; private set; }
        public DateTime DateDonated { get; set; }
        public decimal AmountDonated { get; set; }

        public QuickDonateViewModel(int donorId, IEnumerable<Campaign> campaigns) : this(donorId, campaigns, DateTime.UtcNow) { }

        public QuickDonateViewModel(int donorId, IEnumerable<Campaign> campaigns, DateTime dateDonated, decimal amountDonated = 0m)
        {
            DonorId = donorId;
            Campaigns = campaigns.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            DateDonated = dateDonated;
            AmountDonated = amountDonated;
        }
    }
}