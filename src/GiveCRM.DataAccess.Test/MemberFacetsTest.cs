using System.Collections.Generic;
using System.Linq;
using GiveCRM.Models;
using NUnit.Framework;
using Simple.Data;

namespace GiveCRM.DataAccess.Test
{
    [TestFixture]
    public class MemberFacetsTest
    {
        private readonly dynamic _db = Database.OpenNamedConnection("GiveCRM");

        [SetUp]
        public void SetUp()
        {
            _db.Donations.DeleteAll();
            _db.CampaignRuns.DeleteAll();
            _db.Campaigns.DeleteAll();
            _db.PhoneNumbers.DeleteAll();
            _db.MemberFacetValues.DeleteAll();
            _db.MemberFacets.DeleteAll();
            _db.Members.DeleteAll();
            _db.FacetValues.DeleteAll();
            _db.Facets.DeleteAll();
        }

        [Test]
        public void InsertMemberFacetFreeText()
        {
            var member = CreateBob();

            var textFacet = FacetSetUpHelper.CreateFreeTextFacet();

            var facet = CreateFreeTextMemberFacet(member, textFacet);

            Assert.AreNotEqual(0, facet.Id);
            Assert.AreEqual(textFacet.Id, facet.FacetId);
            Assert.AreEqual("Aardvark", facet.FreeTextValue);
        }

        private static MemberFacetFreeText CreateFreeTextMemberFacet(Member member, Facet textFacet)
        {
            var facet = new MemberFacetFreeText
                            {
                                FacetId = textFacet.Id,
                                MemberId = member.Id,
                                FreeTextValue = "Aardvark"
                            };
            facet = new MemberFacets().Insert(facet);
            return facet;
        }

        [Test]
        public void InsertMemberFacetList()
        {
            var member = CreateBob();

            var listFacet = FacetSetUpHelper.CreateListFacet();

            var facet = CreateListMemberFacet(member, listFacet);

            Assert.AreNotEqual(0, facet.Id);
            Assert.AreEqual(listFacet.Id, facet.FacetId);

            Assert.AreEqual(2, facet.Values.Count);
            Assert.AreEqual(listFacet.Values.First().Id, facet.Values.First().FacetValueId);
            Assert.AreEqual(listFacet.Values.Last().Id, facet.Values.Last().FacetValueId);
        }

        private static MemberFacetList CreateListMemberFacet(Member member, Facet listFacet)
        {
            var facet = new MemberFacetList
                            {
                                FacetId = listFacet.Id,
                                MemberId = member.Id,
                                Values = new List<MemberFacetValue>
                                             {
                                                 new MemberFacetValue {FacetValueId = listFacet.Values.First().Id},
                                                 new MemberFacetValue {FacetValueId = listFacet.Values.Last().Id},
                                             }
                            };
            facet = new MemberFacets().Insert(facet);
            return facet;
        }

        [Test]
        public void AllMemberFacets()
        {
            var member = CreateBob();

            var listFacet = FacetSetUpHelper.CreateListFacet();
            var textFacet = FacetSetUpHelper.CreateFreeTextFacet();

            var expectedMemberListFacet = CreateListMemberFacet(member, listFacet);
            var expectedMemberTextFacet = CreateFreeTextMemberFacet(member, textFacet);

            var facets = new MemberFacets().ForMember(member.Id).ToList();

            var actualMemberListFacet = facets.OfType<MemberFacetList>().FirstOrDefault();
            Assert.IsNotNull(actualMemberListFacet);
            Assert.AreEqual(2, actualMemberListFacet.Values.Count);

            var actualMemberTextFacet = facets.OfType<MemberFacetFreeText>().FirstOrDefault();
            Assert.IsNotNull(actualMemberTextFacet);
        }

        private static Member CreateBob()
        {
            var member = new Member
                             {
                                 Reference = "ABC123",
                                 Title = "Mr",
                                 FirstName = "Bob",
                                 LastName = "Along",
                                 Salutation = "Bob",
                                 EmailAddress = "bob@hotmail.com"
                             };
            member = new Members().Insert(member);
            return member;
        }
    }
}