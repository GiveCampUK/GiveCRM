using System;
using System.Collections.Generic;
using System.Linq;
using GiveCRM.Models;

namespace GiveCRM.BusinessLogic
{
    internal class MemberService : IMemberService
    {
        private readonly IMemberRepository _repository;

        public MemberService(IMemberRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            _repository = repository;
        }

        public IEnumerable<Member> All()
        {
            return _repository.GetAll();
        }

        public Member Get(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(Member member)
        {
            _repository.Update(member);
        }

        public void Insert(Member member)
        {
            _repository.Insert(member);
        }

        public void Save(Member member)
        {
            if (member.Id == 0)
                this.Insert(member);
            else
                this.Update(member);
        }

        public void Delete(Member member)
        {
            member.AddressLine1 = "deleted";
            member.AddressLine2 = "deleted";
            member.EmailAddress = "deleted";
            member.FirstName = "deleted";
            member.LastName = "deleted";

            member.Archived = true;

            _repository.Update(member);
        }

        public IEnumerable<Member> Search(string name, string postcode, string reference)
        {
            var members = _repository.Search(name, postcode, reference);
            return members;
        }

        public IEnumerable<Member> Search(string criteria)
        {
            var results = _repository.GetAll()
                                     .Where(member => !member.Archived && (criteria == string.Empty || NameSearch(member, criteria.ToLower())));

            return results;
        }

        public IEnumerable<Member> FromCampaignRun(int campaignId)
        {
            return _repository.GetByCampaignId(campaignId);
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