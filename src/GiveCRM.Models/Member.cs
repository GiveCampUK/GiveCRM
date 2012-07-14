namespace GiveCRM.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Member
    {
        private decimal totalDonations;
        private Lazy<ICollection<Donation>> lazyDonations;
        public int Id { get; set; }

        public string Reference { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salutation { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public bool Archived { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
        private ICollection<Donation> donations;
        public ICollection<Donation> Donations
        {
            get { return donations ?? (donations = lazyDonations != null ? lazyDonations.Value : null); }
            set { donations = value; }
        }

        public decimal TotalDonations
        {
            get {
                if (donations != null)
                {
                    return donations.Sum(d => d.Amount);
                }
                else
                {
                    return totalDonations;
                }
            }
            set { totalDonations = value; }
        }

        public void SetLazyDonations(Lazy<ICollection<Donation>> lazyDonations)
        {
            this.lazyDonations = lazyDonations;
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", this.Salutation, this.FirstName, this.LastName);
        }
    }
}
