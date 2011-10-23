using System;
using System.Collections.Generic;
using System.Linq;

namespace GiveCRM.Models
{
    public class Member
    {
        private decimal _totalDonations;
        private Lazy<ICollection<Donation>> _lazyDonations;
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
        private ICollection<Donation> _donations;
        public ICollection<Donation> Donations
        {
            get { return _donations ?? (_donations = _lazyDonations != null ? _lazyDonations.Value : null); }
            set { _donations = value; }
        }

        public decimal TotalDonations
        {
            get {
                if (_donations != null)
                {
                    return _donations.Sum(d => d.Amount);
                }
                else
                {
                    return _totalDonations;
                }
            }
            set { _totalDonations = value; }
        }

        public void SetLazyDonations(Lazy<ICollection<Donation>> lazyDonations)
        {
            _lazyDonations = lazyDonations;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}  ref:{3} email:{4}", Title, FirstName, LastName, 
                Reference, EmailAddress);
        }
    }
}
