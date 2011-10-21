using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return member;
        }

        public IEnumerable<Member> All()
        {
            foreach (var record in _db.Members.All())
            {
                Member member = record;
                member.PhoneNumbers = record.PhoneNumbers.ToList<PhoneNumber>();
                yield return member;
            }
        }

        public Member Insert(Member member)
        {
            Member inserted = _db.Members.Insert(member);
            foreach (var phoneNumber in member.PhoneNumbers)
            {
                phoneNumber.MemberId = inserted.Id;
            }
            inserted.PhoneNumbers = _db.PhoneNumbers.Insert(member.PhoneNumbers).ToList<PhoneNumber>();
            return inserted;
        }

        public void Update(Member member)
        {
            _db.Members.UpdateById(member);
            UpdatePhoneNumbers(member);
        }

        private void UpdatePhoneNumbers(Member member)
        {
            bool refetchPhoneNumbers = false;
            foreach (var phoneNumber in member.PhoneNumbers)
            {
                if (phoneNumber.MemberId == 0)
                {
                    _db.PhoneNumbers.Insert(phoneNumber);
                    refetchPhoneNumbers = true;
                }
                else
                {
                    _db.PhoneNumbers.UpdateById(phoneNumber);
                }
            }

            if (refetchPhoneNumbers)
            {
                member.PhoneNumbers = _db.PhoneNumbers.FindByMemberId(member.Id).ToList<PhoneNumber>();
            }
        }
    }
}
