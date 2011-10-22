using System.Collections.Generic;
using System.Linq;
using GiveCRM.Models;
using NUnit.Framework;
using Simple.Data;

namespace GiveCRM.DataAccess.Test
{
    [TestFixture]
    public class FacetsTest
    {
        private readonly dynamic _db = Database.OpenNamedConnection("GiveCRM");

        [SetUp]
        public void SetUp()
        {
            _db.Donations.DeleteAll();
            _db.CampaignRuns.DeleteAll();
            _db.Campaigns.DeleteAll();
            _db.PhoneNumbers.DeleteAll();
            _db.Members.DeleteAll();
            _db.FacetValues.DeleteAll();
            _db.Facets.DeleteAll();
        }

        [Test]
        public void InsertFreeTextFacet()
        {
            var facet = CreateFreeTextFacet();
            facet = new Facets().Get(facet.Id);
            Assert.AreNotEqual(0, facet.Id);
            Assert.AreEqual(FacetType.FreeText, facet.Type);
            Assert.AreEqual("FreeTextTest", facet.Name);
        }

        private static Facet CreateFreeTextFacet()
        {
            var facets = new Facets();
            var facet = new Facet
                            {
                                Type = FacetType.FreeText,
                                Name = "FreeTextTest"
                            };
            var record = facets.Insert(facet);
            facet = record;
            return facet;
        }

        [Test]
        public void InsertListFacet()
        {
            var facet = CreateListFacet();
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
            var facet = CreateListFacet();

            facet = new Facets().Get(facet.Id);
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
            CreateFreeTextFacet();
            CreateListFacet();

            var facet = new Facets().All().FirstOrDefault(f => f.Type == FacetType.FreeText);
            Assert.IsNotNull(facet);
            Assert.AreEqual("FreeTextTest", facet.Name);

            facet = new Facets().All().FirstOrDefault(f => f.Type == FacetType.List);
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

        private static Facet CreateListFacet()
        {
            var facets = new Facets();
            var facet = new Facet
                            {
                                Type = FacetType.List,
                                Name = "ListTest",
                                Values = new List<FacetValue>
                                             {
                                                 new FacetValue {Value = "One"},
                                                 new FacetValue {Value = "Two"},
                                             }
                            };
            var record = facets.Insert(facet);
            facet = record;
            return facet;
        }
    }
}