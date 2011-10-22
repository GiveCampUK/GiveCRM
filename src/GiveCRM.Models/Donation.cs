using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveCRM.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int? CampaignId { get; set; }
    }
}
