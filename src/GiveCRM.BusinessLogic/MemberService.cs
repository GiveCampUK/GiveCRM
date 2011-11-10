using System;
using System.Collections.Generic;
using System.Linq;
using GiveCRM.Models;
using GiveCRM.Models.Search;

namespace GiveCRM.BusinessLogic
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository memberRepository;
        private readonly IMemberSearchFilterRepository memberSearchFilterRepository;
        private readonly ISearchQueryService searchQueryService;

        public MemberService(IMemberRepository memberRepository, IMemberSearchFilterRepository memberSearchFilterRepository, ISearchQueryService searchQueryService)
        {
            if (memberRepository == null)
            {
                throw new ArgumentNullException("memberRepository");
            }
            
            if (memberSearchFilterRepository == null)
            {
                throw new ArgumentNullException("memberSearchFilterRepository");
            }

            if (searchQueryService == null)
            {
                throw new ArgumentNullException("searchQueryService");
            }

            this.memberRepository = memberRepository;
            this.memberSearchFilterRepository = memberSearchFilterRepository;
            this.searchQueryService = searchQueryService;
        }

        public IEnumerable<Member> All()
        {
            return memberRepository.GetAll();
        }

        public Member Get(int id)
        {
            return memberRepository.GetById(id);
        }

        public void Update(Member member)
        {
            memberRepository.Update(member);
        }

        public void Insert(Member member)
        {
            memberRepository.Insert(member);
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

            memberRepository.Update(member);
        }

        public IEnumerable<Member> Search(string name, string postcode, string reference)
        {
            var members = memberRepository.Search(name, postcode, reference);
            return members;
        }

        public IEnumerable<Member> Search(string criteria)
        {
            var results = memberRepository.GetAll()
                                     .Where(member => !member.Archived && (criteria == string.Empty || NameSearch(member, criteria.ToLower())));

            return results;
        }

        public IEnumerable<Member> Search(IEnumerable<SearchCriteria> criteria)
        {
            var criteriaList = criteria.ToList();

            if (criteriaList.Count == 0)
            {
                // don't attempt to search if there are not criteria - don't want the whole database
                return Enumerable.Empty<Member>();
            }

            var query = searchQueryService.CompileQuery(criteriaList);

            return query.Cast<Member>();
        }

        public IEnumerable<Member> SearchByCampaignId(int campaignId)
        {
            var filters = memberSearchFilterRepository.GetByCampaignId(campaignId).Select(msf => msf.ToSearchCriteria());
            return Search(filters);
        }

        public IEnumerable<Member> FromCampaignRun(int campaignId)
        {
            return memberRepository.GetByCampaignId(campaignId);
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