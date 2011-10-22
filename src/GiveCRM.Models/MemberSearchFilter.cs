using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveCRM.Models
{
    public class MemberSearchFilter
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public string LeftHandSide { get; set; }
        public string Operator { get; set; }
        public string RightHandSide { get; set; }
    }
}
