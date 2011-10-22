using System;

namespace GiveCRM.Models
{
    public class Campaign
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? RunOn { get; set; }
        public char IsClosed { get; set; }

        public override string ToString()
        {
            return Id + " " + Name;
        }
    }
}
