using System;
using System.Collections.Generic;
using GiveCRM.BusinessLogic;
using GiveCRM.Models;
using Simple.Data;

namespace GiveCRM.DataAccess
{
    public class Members : IMemberRepository
    {
        private readonly dynamic db = Database.OpenNamedConnection("GiveCRM");

        public Member GetById(int id)
        {
            var record = db.Members.FindById(id);
            Member member = record;
            member.PhoneNumbers = record.PhoneNumbers.ToList<PhoneNumber>();
            member.Donations = record.Donations.ToList<Donation>();
            return member;
        }

        public IEnumerable<Member> GetAll()
        {
            var query = db.Members.All().OrderBy(db.Members.Id);
            return RunMemberQueryWithPhoneNumbers(query);
        }

        public IEnumerable<Member> Search(string lastName, string postalCode, string reference)
        {
            var query = db.Members.All();

            if(!String.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(db.Members.LastName.Like(lastName + "%"));
            }

            if(!String.IsNullOrWhiteSpace(postalCode))
            {
                query = query.Where(db.Members.PostalCode.Like(postalCode + "%")); 
            }

            if(!String.IsNullOrWhiteSpace(reference)) 
            {
                query = query.Where(db.Members.Reference.Like(reference + "%")); 
            }

            return query.ToList<Member>(); 
        }

        public IEnumerable<Member> GetByCampaignId(int campaignId)
        {
            var query = db.Members.All()
                .Where(db.Members.CampaignRun.CampaignId == campaignId)
                .OrderBy(db.Members.Id);
            return RunMemberQueryWithPhoneNumbers(query);
        }

        private IEnumerable<Member> RunMemberQueryWithPhoneNumbers(dynamic query)
        {
            query = query.Select(db.Members.Id, db.Members.Reference, db.Members.Title, db.Members.FirstName,
                                 db.Members.LastName, db.Members.Salutation, db.Members.EmailAddress,
                                 db.Members.AddressLine1, db.Members.AddressLine2, db.Members.City,
                                 db.Members.Region,
                                 db.Members.PostalCode, db.Members.Country,
                                 db.Members.Archived,
                                 db.Members.PhoneNumbers.Id.As("PhoneNumberId"),
                                 db.Members.PhoneNumbers.PhoneNumberType,
                                 db.Members.PhoneNumbers.Number,
                                 db.Members.Donations.Amount.Sum().As("TotalDonations"));

            Member member = null;

            foreach (dynamic record in query)
            {
                if (member != null && member.Id != record.Id)
                {
                    yield return member;
                    member = null;
                }
                if (member == null)
                {
                    member = record;
                    member.PhoneNumbers = new List<PhoneNumber>();
                }

                if (record.PhoneNumberId != null)
                {
                    member.PhoneNumbers.Add(new PhoneNumber
                                                {
                                                    Id = record.PhoneNumberId ?? 0,
                                                    MemberId = member.Id,
                                                    Number = record.Number ?? string.Empty,
                                                    PhoneNumberType = (PhoneNumberType) record.PhoneNumberType
                                                });
                }
            }

            if (member != null)
            {
                yield return member;
            }
        }

        public Member Insert(Member member)
        {
            if (member.PhoneNumbers != null && member.PhoneNumbers.Count > 0)
            {
                return InsertWithPhoneNumbers(member);
            }
            return db.Members.Insert(member);
        }

        /// <summary>
        /// Deletes the item of type <typeparamref name="T"/> identified by the specified identifier.  
        /// </summary>
        /// <param name="id">The identifier of the item to delete.</param>
        public void DeleteById(int id)
        {
            _db.Members.DeleteById(id);
        }

        private Member InsertWithPhoneNumbers(Member member)
        {
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    Member inserted = transaction.Members.Insert(member);

                    foreach (var phoneNumber in member.PhoneNumbers)
                    {
                        phoneNumber.MemberId = inserted.Id;
                    }
                    inserted.PhoneNumbers =
                        transaction.PhoneNumbers.Insert(member.PhoneNumbers).ToList<PhoneNumber>();
                    transaction.Commit();
                    return inserted;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Update(Member member)
        {
            bool refetchPhoneNumbers = false;

            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    transaction.Members.UpdateById(member);
                    refetchPhoneNumbers = UpdatePhoneNumbers(member, transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }

            if (refetchPhoneNumbers)
            {
                var newNos = db.PhoneNumbers.FindAllByMemberId(member.Id);
                member.PhoneNumbers = newNos.ToList<PhoneNumber>();
            }

        }

        private bool UpdatePhoneNumbers(Member member, dynamic transaction)
        {
            bool refetchPhoneNumbers = false; 
            
            foreach (var phoneNumber in member.PhoneNumbers)
            {
                if (phoneNumber.MemberId == 0)
                {
                    phoneNumber.MemberId = member.Id;
                    transaction.PhoneNumbers.Insert(phoneNumber);
                    refetchPhoneNumbers = true; 
                }
                else
                {
                    transaction.PhoneNumbers.UpdateById(phoneNumber);
                }
            }

            return refetchPhoneNumbers; 
        }
        
    }
}
