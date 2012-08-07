namespace GiveCRM.DataAccess.Test
{
    using System.Linq;
    using GiveCRM.Models;
    using NUnit.Framework;
    using Simple.Data;

    [TestFixture]
    public class FacetsTest
    {
        private IDatabaseProvider databaseProvider;

        [SetUp]
        public void SetUp()
        {
            databaseProvider = new InMemorySingleTenantDatabaseProvider()
                .Configure(adapter =>
                               {
                                   adapter.SetAutoIncrementKeyColumn("Facets", "Id");
                                   adapter.SetAutoIncrementKeyColumn("FacetValues", "Id");
                                   adapter.Join.Master("Facets", "Id").Detail("FacetValues", "FacetId", "Values");
                               });

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
        [Ignore("Ignored until a new version of Simple.Data is available to fix issues with InMemoryAdapter.")]
        public void InsertFreeTextFacet()
        {
            var facet = FacetSetUpHelper.CreateFreeTextFacet(databaseProvider);
            facet = new Facets(databaseProvider).GetById(facet.Id);
            Assert.AreNotEqual(0, facet.Id);
            Assert.AreEqual(FacetType.FreeText, facet.Type);
            Assert.AreEqual("FreeTextTest", facet.Name);
        }

        [Test]
        [Ignore("Ignored until a new version of Simple.Data is available to fix issues with InMemoryAdapter.")]
        public void InsertListFacet()
        {
            var facet = FacetSetUpHelper.CreateListFacet(databaseProvider);
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
        [Ignore("Ignored until a new version of Simple.Data is available to fix issues with InMemoryAdapter.")]
        public void GetListFacet()
        {
            var facet = FacetSetUpHelper.CreateListFacet(databaseProvider);

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
        [Ignore("Ignored until a new version of Simple.Data is available to fix issues with InMemoryAdapter.")]
        public void AllFacets()
        {
            FacetSetUpHelper.CreateFreeTextFacet(databaseProvider);
            FacetSetUpHelper.CreateListFacet(databaseProvider);

            var facets = new Facets(databaseProvider).GetAll();

            var facet = facets.FirstOrDefault(f => f.Type == FacetType.FreeText);
            Assert.IsNotNull(facet);
            Assert.AreEqual("FreeTextTest", facet.Name);

            facet = facets.FirstOrDefault(f => f.Type == FacetType.List);
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