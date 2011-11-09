using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveCRM.Models;
using GiveCRM.Models.Search;
using GiveCRM.Web.Models.Search;
using NSubstitute;
using NUnit.Framework;

namespace GiveCRM.BusinessLogic.Tests
{
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
            SubstituteExtensions.Returns(searchQueryService.CompileQuery(searchCriteria), searchResults);
            
            var memberRepository = Substitute.For<IMemberRepository>();
            var memberSearchFilterRepository = Substitute.For<IMemberSearchFilterRepository>();
            
            var memberService = new MemberService(memberRepository, memberSearchFilterRepository, searchQueryService);
            var results = memberService.Search(searchCriteria);

            CollectionAssert.AreEqual(searchResults, results);
        }

        [Test]
        public void Search_ShouldReturnAnEmptyList_WhenTheSearchCriteriaMatchNoMembers()
        {
            var searchResults = Enumerable.Empty<Member>();
            var searchCriteria = new[] { surnameSearchCriteria };

            var searchQueryService = Substitute.For<ISearchQueryService>();
            SubstituteExtensions.Returns(searchQueryService.CompileQuery(searchCriteria), searchResults);

            var memberRepository = Substitute.For<IMemberRepository>();
            var memberSearchFilterRepository = Substitute.For<IMemberSearchFilterRepository>();

            var memberService = new MemberService(memberRepository, memberSearchFilterRepository, searchQueryService);
            var results = memberService.Search(searchCriteria);

            CollectionAssert.AreEqual(searchResults, results);
        }
    }
}
