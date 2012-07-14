﻿namespace GiveCRM.Models
{
    using System;

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

        private string isClosed = "N";
        public string IsClosed
        {
            get { return isClosed; }
            set { isClosed = value; }
        }

        public override string ToString()
        {
            return Id + " " + Name;
        }

        public bool IsReadonly
        {
            get { return IsCommitted || (IsClosed == "Y"); }
        }
    }
}
