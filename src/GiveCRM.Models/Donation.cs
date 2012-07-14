namespace GiveCRM.Models
{
    using System;
    using System.ComponentModel;

    public class Donation
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        [DisplayName("Campaign")]
        public int? CampaignId { get; set; }

        public override string ToString()
        {
            return Id + " " + MemberId + " " + Amount + " " + Date.ToString();
        }
    }
}
