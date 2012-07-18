namespace GiveCRM.DataAccess.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using GiveCRM.Models;
    using NUnit.Framework;

    [TestFixture]
    public class MemberFacetsTest
    {
        private IDatabaseProvider databaseProvider;

        [SetUp]
        public void SetUp()
        {
            databaseProvider = new InMemorySingleTenantDatabaseProvider();
            dynamic db = databaseProvider.GetDatabase();

            db.Donations.DeleteAll();
            db.CampaignRuns.DeleteAll();
            db.Campaigns.DeleteAll();
            db.PhoneNumbers.DeleteAll();
            db.MemberFacetValues.DeleteAll();
            db.MemberFacets.DeleteAll();
            db.Members.DeleteAll();
            db.FacetValues.DeleteAll();
            db.Facets.DeleteAll();
        }

        [Test]
        public void InsertMemberFacetFreeText()
        {
            var member = CreateBob();

            var textFacet = FacetSetUpHelper.CreateFreeTextFacet(databaseProvider);

            var facet = CreateFreeTextMemberFacet(member, textFacet);

            Assert.AreNotEqual(0, facet.Id);
            Assert.AreEqual(textFacet.Id, facet.FacetId);
            Assert.AreEqual("Aardvark", facet.FreeTextValue);
        }

        private MemberFacetFreeText CreateFreeTextMemberFacet(Member member, Facet textFacet)
        {
            var facet = new MemberFacetFreeText
                            {
                                FacetId = textFacet.Id,
                                MemberId = member.Id,
                                FreeTextValue = "Aardvark"
                            };
            facet = new MemberFacets(databaseProvider).Insert(facet);
            return facet;
        }

        [Test]
        public void InsertMemberFacetList()
        {
            var member = CreateBob();

            var listFacet = FacetSetUpHelper.CreateListFacet(databaseProvider);

            var facet = CreateListMemberFacet(member, listFacet);

            Assert.AreNotEqual(0, facet.Id);
            Assert.AreEqual(listFacet.Id, facet.FacetId);

            Assert.AreEqual(2, facet.Values.Count);
            Assert.AreEqual(listFacet.Values.First().Id, facet.Values.First().FacetValueId);
            Assert.AreEqual(listFacet.Values.Last().Id, facet.Values.Last().FacetValueId);
        }

        private MemberFacetList CreateListMemberFacet(Member member, Facet listFacet)
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
            facet = new MemberFacets(databaseProvider).Insert(facet);
            return facet;
        }

        [Test]
        public void AllMemberFacets()
        {
            var member = CreateBob();

            var listFacet = FacetSetUpHelper.CreateListFacet(databaseProvider);
            var textFacet = FacetSetUpHelper.CreateFreeTextFacet(databaseProvider);

            var expectedMemberListFacet = CreateListMemberFacet(member, listFacet);
            var expectedMemberTextFacet = CreateFreeTextMemberFacet(member, textFacet);

            var facets = new MemberFacets(databaseProvider).ForMember(member.Id).ToList();

            var actualMemberListFacet = facets.OfType<MemberFacetList>().FirstOrDefault();
            Assert.IsNotNull(actualMemberListFacet);
            Assert.AreEqual(2, actualMemberListFacet.Values.Count);

            var actualMemberTextFacet = facets.OfType<MemberFacetFreeText>().FirstOrDefault();
            Assert.IsNotNull(actualMemberTextFacet);
        }

        private Member CreateBob()
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
            member = new Members(databaseProvider).Insert(member);
            return member;
        }
    }
}