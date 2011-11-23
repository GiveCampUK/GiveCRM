using System.Collections.Generic;
using System.Linq;
using GiveCRM.DataAccess;
using GiveCRM.Models;

namespace GiveCRM.Web.Services
{
    public class MemberService: IMemberService
    {
        private readonly Members membersDb = new Members();

        public IEnumerable<Member> All()
        {
            return membersDb.All();
        }

        public Member Get(int id)
        {
            var member = membersDb.Get(id);
            return member;
        }

        public void Update(Member member)
        {
            membersDb.Update(member);
        }

        public void Insert(Member member)
        {
            membersDb.Insert(member);
        }

        public void Save(Member member)
        {
            if (member.Id == 0)
            {
                Insert(member);
            }
            else
            {
                Update(member);
            }
        }

        public void Delete(Member member)
        {
            member.AddressLine1 = "deleted";
            member.AddressLine2 = "deleted";
            member.EmailAddress = "deleted";
            member.FirstName = "deleted";
            member.LastName = "deleted";

            member.Archived = true;

            membersDb.Update(member);
        }

        public IEnumerable<Member> Search(string name, string postcode, string reference)
        {
            var members = membersDb.Search(name, postcode, reference);
            return members;
        }

        public IEnumerable<Member> Search(string criteria)
        {
            var results = membersDb
                .All()
                .Where(member =>
                    !member.Archived &&
                    (criteria == string.Empty || NameSearch(member, criteria.ToLower())));

            return results;
        }

        public IEnumerable<Member> FromCampaignRun(int campaignId)
        {
            return membersDb.FromCampaignRun(campaignId);
        }

        private bool NameSearch(Member member, string criteria)
        {
            return GetForenameSurname(member).Contains(criteria) || GetSurnameForename(member).Contains(criteria) || GetInitialSurname(member).Contains(criteria);
        }

        private string GetForenameSurname(Member member)
        {
            return string.Format("{0} {1} {2}", member.Salutation, member.FirstName, member.LastName).ToLower();
        }

        private string GetSurnameForename(Member member)
        {
            return string.Format("{0} {1} {2}", member.Salutation, member.LastName, member.FirstName).ToLower();
        }

        private string GetInitialSurname(Member member)
        {
            return string.Format("{0} {1} {2}", member.Salutation, member.FirstName.Substring(0, 1), member.LastName).ToLower();
        }



    }
}