using System;
using System.ComponentModel.DataAnnotations; 
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 

using GiveCRM.Models; 

namespace GiveCRM.Web.Models.Members
{
    public class MemberEditViewModel
    {
        public MemberEditViewModel()
        {
            this.PhoneNumbers = new List<PhoneNumber>(); 
        }

        public int Id { get; set; }

        [Required] 
        [StringLength(40)] 
        public string Reference { get; set; }
        [StringLength(40)]
        public string Title { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name="Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]       
        public string Salutation { get; set; }
        [StringLength(100)]
        [RegularExpression("^[^\\s]+@[^\\s]+$", ErrorMessage="Enter a valid email address")] 
        [Display(Name="Email Address")]
        public string EmailAddress { get; set; }
        [StringLength(100)]
        [Display(Name="Address Line 1")]
        public string AddressLine1 { get; set; }
        [StringLength(100)]
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(100)]
        public string Region { get; set; }
        [StringLength(100)]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [StringLength(100)]
        public string Country { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }

        public Member ToModel()
        {
            return new Member()
            {
                Id = this.Id, 
                Reference = this.Reference, 
                Title = this.Title, 
                FirstName = this.FirstName, 
                LastName = this.LastName,
                Salutation = this.Salutation, 
                EmailAddress = this.EmailAddress, 
                AddressLine1 = this.AddressLine1,
                AddressLine2 = this.AddressLine2, 
                City = this.City,
                Region = this.Region, 
                PostalCode = this.PostalCode,
                Country = this.Country,
                PhoneNumbers = this.PhoneNumbers
            };
        }

        public static MemberEditViewModel ToViewModel(Member member)
        {
            return new MemberEditViewModel()
            {
                Id = member.Id,
                Reference = member.Reference,
                Title = member.Title,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Salutation = member.Salutation,
                EmailAddress = member.EmailAddress,
                AddressLine1 = member.AddressLine1,
                AddressLine2 = member.AddressLine2,
                City = member.City,
                Region = member.Region,
                PostalCode = member.PostalCode,
                Country = member.Country,
                PhoneNumbers = member.PhoneNumbers
            }; 
        }
    }
}