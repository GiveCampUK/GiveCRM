﻿namespace GiveCRM.BusinessLogic.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using GiveCRM.Models;
    using GiveCRM.Models.Search;
    using GiveCRM.Web.Models.Search;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class MemberServiceTests
    {
        private readonly Member member1 = new Member
                                              {
                                                  Id = 1,
                                                  FirstName = "Joe",
                                                  LastName = "Bloggs"
                                              };

        private readonly Member member2 = new Member
                                              {
                                                  Id = 2,
                                                  FirstName = "Jane",
                                                  LastName = "Bloggs"
                                              };

        private readonly SearchCriteria surnameSearchCriteria = new CampaignSearchCriteria
                                                                    {
                                                                        SearchOperator = SearchOperator.EqualTo,
                                                                        Type = SearchFieldType.String,
                                                                        DisplayName = "Surname equals Bloggs",
                                                                        Value = "Bloggs"
                                                                    };

        [Test]
        public void All_ShouldReturnAListOfAllTheMembers_WhenThereAreMembers()
        {
            var allMembers = new[] { member1, member2 };
            var memberRepository = Substitute.For<IMemberRepository>();
            memberRepository.GetAll().Returns(allMembers);

            var memberSearchFilterRepository = Substitute.For<IMemberSearchFilterRepository>();
            var searchQueryService = Substitute.For<ISearchQueryService>();

            var memberService = new MemberService(memberRepository, memberSearchFilterRepository, searchQueryService);
            var results = memberService.All();

            CollectionAssert.AreEqual(allMembers, results);
        }

        [Test]
        public void All_ShouldReturnAnEmptyList_WhenThereAreNoMembers()
        {
            var allMembers = Enumerable.Empty<Member>();
            var memberRepository = Substitute.For<IMemberRepository>();
            memberRepository.GetAll().Returns(allMembers);

            var memberSearchFilterRepository = Substitute.For<IMemberSearchFilterRepository>();
            var searchQueryService = Substitute.For<ISearchQueryService>();

            var memberService = new MemberService(memberRepository, memberSearchFilterRepository, searchQueryService);
            var results = memberService.All();

            CollectionAssert.AreEqual(allMembers, results);
        }

        [Test]
        public void Get_ShouldReturnTheSpecifiedMember_WhenTheMemberExists()
        {
            const int memberId = 1;
            
            var memberRepository = Substitute.For<IMemberRepository>();
            memberRepository.GetById(memberId).Returns(member1);

            var memberSearchFilterRepository = Substitute.For<IMemberSearchFilterRepository>();
            var searchQueryService = Substitute.For<ISearchQueryService>();

            var memberService = new MemberService(memberRepository, memberSearchFilterRepository, searchQueryService);
            var member = memberService.Get(memberId);

            Assert.AreEqual(member1, member);
        }

        [Test]
        public void Get_ShouldReturnNull_WhenTheMemberDoesNotExist()
        {
            const int memberId = 3;
            
            var memberRepository = Substitute.For<IMemberRepository>();
            memberRepository.GetById(memberId).Returns((Member)null);

            var memberSearchFilterRepository = Substitute.For<IMemberSearchFilterRepository>();
            var searchQueryService = Substitute.For<ISearchQueryService>();

            var memberService = new MemberService(memberRepository, memberSearchFilterRepository, searchQueryService);
            var member = memberService.Get(memberId);

            Assert.IsNull(member);
        }

        [Test]
        public void Search_ShouldReturnAListOfMembersMatchingTheSpecifedSearchCriteria_WhenTheSearchCriteriaAreValid()
        {
            var searchResults = new[] { member1, member2 };
            var searchCriteria = new[] { surnameSearchCriteria };

            var searchQueryService = Substitute.For<ISearchQueryService>();
            searchQueryService.CompileQuery<Member>(Arg.Any<IEnumerable<SearchCriteria>>()).Returns(searchResults);
            
            var memberRepository = Substitute.For<IMemberRepository>();
            var memberSearchFilterRepository = Substitute.For<IMemberSearchFilterRepository>();
            
            var memberService = new MemberService(memberRepository, memberSearchFilterRepository, searchQueryService);
            var results = memberService.Search(searchCriteria);

            CollectionAssert.AreEqual(searchResults, results.ToArray());
        }

        [Test]
        public void Search_ShouldReturnAnEmptyList_WhenTheSearchCriteriaMatchNoMembers()
        {
            var searchResults = Enumerable.Empty<Member>();
            var searchCriteria = new[] { surnameSearchCriteria };

            var searchQueryService = Substitute.For<ISearchQueryService>();
            searchQueryService.CompileQuery<Member>(searchCriteria).Returns(searchResults);

            var memberRepository = Substitute.For<IMemberRepository>();
            var memberSearchFilterRepository = Substitute.For<IMemberSearchFilterRepository>();

            var memberService = new MemberService(memberRepository, memberSearchFilterRepository, searchQueryService);
            var results = memberService.Search(searchCriteria);

            CollectionAssert.AreEqual(searchResults, results);
        }

        [Test]
        public void Search_ShouldReturnAnEmptyList_WhenThereAreNoSearchCriteria()
        {
            var memberRepository = Substitute.For<IMemberRepository>();
            var memberSearchFilterRepository = Substitute.For<IMemberSearchFilterRepository>();
            var searchQueryService = Substitute.For<ISearchQueryService>();

            var memberService = new MemberService(memberRepository, memberSearchFilterRepository, searchQueryService);
            var results = memberService.Search(Enumerable.Empty<SearchCriteria>());

            CollectionAssert.AreEqual(Enumerable.Empty<Member>(), results);
        }

        [Test]
        public void SearchByCampaignId_ShouldReturnAListOfMembers_WhenThereAreMembersInTheCampaign()
        {
            const int campaignId = 1;
            
            var membersInCampaign1 = new[] { member1, member2 };

            var memberSearchFilterRepository = Substitute.For<IMemberSearchFilterRepository>();
            memberSearchFilterRepository.GetByCampaignId(campaignId).Returns(new[]
                                                                                 {
                                                                                     new MemberSearchFilter
                                                                                         {
                                                                                             CampaignId = 1,
                                                                                             DisplayName = "Previously donated to acampaign",
                                                                                             InternalName = "donatedToCampaign"
                                                                                         }
                                                                                 });
            var memberRepository = Substitute.For<IMemberRepository>();
            var searchQueryService = Substitute.For<ISearchQueryService>();
            searchQueryService.CompileQuery<Member>(Arg.Any<IEnumerable<SearchCriteria>>()).Returns(membersInCampaign1);
            
            var memberService = new MemberService(memberRepository, memberSearchFilterRepository, searchQueryService);
            var results = memberService.SearchByCampaignId(campaignId);

            CollectionAssert.AreEqual(membersInCampaign1, results.ToArray());
        }

        [Test]
        public void SearchByCampaignId_ShouldReturnAnEmptyList_WhenThereAreNoMembersInTheCampaign()
        {
            const int campaignId = 2;

            var membersInCampaign2 = Enumerable.Empty<Member>();

            var memberRepository = Substitute.For<IMemberRepository>();
            var memberSearchFilterRepository = Substitute.For<IMemberSearchFilterRepository>();
            var searchQueryService = Substitute.For<ISearchQueryService>();

            var memberService = new MemberService(memberRepository, memberSearchFilterRepository, searchQueryService);

            var results = memberService.SearchByCampaignId(campaignId);

            CollectionAssert.AreEqual(membersInCampaign2, results);
        }

        [Test]
        public void SearchByCampaignId_ShouldReturnAnEmptyList_WhenThereAreNoMembersAtAll()
        {
            const int campaignId = 3;

            var membersInCampaign3 = Enumerable.Empty<Member>();

            var memberRepository = Substitute.For<IMemberRepository>();
            var memberSearchFilterRepository = Substitute.For<IMemberSearchFilterRepository>();
            var searchQueryService = Substitute.For<ISearchQueryService>();

            var memberService = new MemberService(memberRepository, memberSearchFilterRepository, searchQueryService);

            var results = memberService.SearchByCampaignId(campaignId);

            CollectionAssert.AreEqual(membersInCampaign3, results);
        }

        [Test]
        public void FromCampaignRun_Should()
        {
            // Duplicate SearchByCampaignId??
        }
    }
}
