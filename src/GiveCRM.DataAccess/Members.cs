using System.Collections.Generic;

using GiveCRM.Models;
using Simple.Data;

namespace GiveCRM.DataAccess
{
    public class Members
    {
        private readonly dynamic _db = Database.OpenNamedConnection("GiveCRM");

        public Member Get(int id)
        {
            var record = _db.Members.FindById(id);
            Member member = record;
            member.PhoneNumbers = record.PhoneNumbers.ToList<PhoneNumber>();
            member.Donations = record.Donations.ToList<Donation>();
            return member;
        }

        public IEnumerable<Member> All()
        {
            var query = _db.Members.All()
                .Select(_db.Members.Id, _db.Members.Reference, _db.Members.Title, _db.Members.FirstName,
                        _db.Members.LastName, _db.Members.Salutation, _db.Members.EmailAddress,
                        _db.Members.AddressLine1, _db.Members.AddressLine2, _db.Members.City, _db.Members.Region,
                        _db.Members.PostalCode, _db.Members.Country,
                        _db.Members.Archived,
                        _db.Members.PhoneNumbers.Id.As("PhoneNumberId"), _db.Members.PhoneNumbers.Type,
                        _db.Members.PhoneNumbers.Number,
                        _db.Members.Donations.Amount.Sum().As("TotalDonations"))
                .OrderBy(_db.Members.Id);

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
                member.PhoneNumbers.Add(new PhoneNumber { Id = record.PhoneNumberId, MemberId = member.Id, Number = record.Number, Type = record.Type });
            }

            yield return member;
        }

        public Member Insert(Member member)
        {
            if (member.PhoneNumbers != null && member.PhoneNumbers.Count > 0)
            {
                return InsertWithPhoneNumbers(member);
            }
            return _db.Members.Insert(member);
        }

        private Member InsertWithPhoneNumbers(Member member)
        {
            using (var transaction = _db.BeginTransaction())
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

            using (var transaction = _db.BeginTransaction())
            {
                try
                {
                    // TODO: change this to check if there are any new numbers
                    //if (member.Id == 0)
                        refetchPhoneNumbers = true;
                    
                    transaction.Members.UpdateById(member);
                    UpdatePhoneNumbers(member, transaction);
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
                var newNos = _db.PhoneNumbers.FindAllByMemberId(member.Id);
                member.PhoneNumbers = newNos.ToList<PhoneNumber>();
            }

        }

        private void UpdatePhoneNumbers(Member member, dynamic transaction)
        {
            
            foreach (var phoneNumber in member.PhoneNumbers)
            {
                if (phoneNumber.MemberId == 0)
                {
                    phoneNumber.MemberId = member.Id;
                    transaction.PhoneNumbers.Insert(phoneNumber);
                    
                }
                else
                {
                    transaction.PhoneNumbers.UpdateById(phoneNumber);
                }
            }
        }


    }
}
