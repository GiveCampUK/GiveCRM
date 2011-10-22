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
            foreach (dynamic record in _db.Members.All())
            {
                Member member = record;
                member.PhoneNumbers = _db.PhoneNumbers.FindAllByMemberId(member.Id).ToList<PhoneNumber>(); // record.PhoneNumbers.ToList<PhoneNumber>();
                member.Donations = _db.Donations.FindAllByMemberId(member.Id).ToList<Donation>();
                yield return member;
            }
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
                var newNos = _db.PhoneNumbers.FindAllByMemberId(member.Id);
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
