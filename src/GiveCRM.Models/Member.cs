using System.Collections.Generic;

namespace GiveCRM.Models
{
    public class Member
    {
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
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} ({3})", Title, FirstName, LastName, EmailAddress);
        }
    }
}
