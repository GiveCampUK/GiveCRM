using System;

namespace GiveCRM.Models
{
    public class Campaign
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? RunOn { get; set; }

        public bool IsCommitted
        {
            get { return RunOn.HasValue; }
        }

        private string _isClosed = "N";
        public string IsClosed
        {
            get { return _isClosed; }
            set { _isClosed = value; }
        }

        public override string ToString()
        {
            return Id + " " + Name;
        }
    }
}
