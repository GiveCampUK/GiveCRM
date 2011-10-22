using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveCRM.Models
{
    public class Campaign
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? RunOn { get; set; }
        public char IsClosed { get; set; }
    }
}
