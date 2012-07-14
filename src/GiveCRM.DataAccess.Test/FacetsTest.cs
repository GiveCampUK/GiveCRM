namespace GiveCRM.DataAccess.Test
{
    using System.Linq;
    using GiveCRM.Models;
    using NUnit.Framework;

    [TestFixture]
    public class FacetsTest
    {
        private IDatabaseProvider databaseProvider;

        [SetUp]
        public void SetUp()
        {
            databaseProvider = new SingleTenantDatabaseProvider();
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
        public void InsertFreeTextFacet()
        {
            var facet = FacetSetUpHelper.CreateFreeTextFacet();
            facet = new Facets(databaseProvider).GetById(facet.Id);
            Assert.AreNotEqual(0, facet.Id);
            Assert.AreEqual(FacetType.FreeText, facet.Type);
            Assert.AreEqual("FreeTextTest", facet.Name);
        }

        [Test]
        public void InsertListFacet()
        {
            var facet = FacetSetUpHelper.CreateListFacet();
            Assert.AreNotEqual(0, facet.Id);
            Assert.AreEqual(FacetType.List, facet.Type);
            Assert.AreEqual("ListTest", facet.Name);
            Assert.IsNotNull(facet.Values);
            Assert.AreEqual(1, facet.Values.Count(v => v.Value == "One"));
            Assert.AreEqual(1, facet.Values.Count(v => v.Value == "Two"));
            foreach (var value in facet.Values)
            {
                Assert.AreEqual(facet.Id, value.FacetId);
                Assert.AreNotEqual(0, value.Id);
            }
        }

        [Test]
        public void GetListFacet()
        {
            var facet = FacetSetUpHelper.CreateListFacet();

            facet = new Facets(databaseProvider).GetById(facet.Id);
            Assert.AreNotEqual(0, facet.Id);
            Assert.AreEqual(FacetType.List, facet.Type);
            Assert.AreEqual("ListTest", facet.Name);
            Assert.IsNotNull(facet.Values);
            Assert.AreEqual(1, facet.Values.Count(v => v.Value == "One"));
            Assert.AreEqual(1, facet.Values.Count(v => v.Value == "Two"));
            foreach (var value in facet.Values)
            {
                Assert.AreEqual(facet.Id, value.FacetId);
                Assert.AreNotEqual(0, value.Id);
            }
        }

        [Test]
        public void AllFacets()
        {
            FacetSetUpHelper.CreateFreeTextFacet();
            FacetSetUpHelper.CreateListFacet();

            var facet = new Facets(databaseProvider).GetAll().FirstOrDefault(f => f.Type == FacetType.FreeText);
            Assert.IsNotNull(facet);
            Assert.AreEqual("FreeTextTest", facet.Name);

            facet = new Facets(databaseProvider).GetAll().FirstOrDefault(f => f.Type == FacetType.List);
            Assert.IsNotNull(facet);
            Assert.AreNotEqual(0, facet.Id);
            Assert.AreEqual(FacetType.List, facet.Type);
            Assert.AreEqual("ListTest", facet.Name);
            Assert.IsNotNull(facet.Values);
            Assert.AreEqual(1, facet.Values.Count(v => v.Value == "One"));
            Assert.AreEqual(1, facet.Values.Count(v => v.Value == "Two"));
            foreach (var value in facet.Values)
            {
                Assert.AreEqual(facet.Id, value.FacetId);
                Assert.AreNotEqual(0, value.Id);
            }
        }
    }
}