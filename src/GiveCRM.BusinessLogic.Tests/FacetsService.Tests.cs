namespace GiveCRM.BusinessLogic.Tests
{
    using System.Linq;
    using GiveCRM.Models;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class FacetsServiceTests
    {
        private readonly Facet favouriteAnimal = new Facet { Id = 1, Name = "Favourite Animal", Type = FacetType.FreeText };

        private readonly Facet bigFeet = new Facet
                                             {
                                                 Id = 2,
                                                 Name = "Has Big Feet",
                                                 Type = FacetType.List,
                                                 Values = new[]
                                                              {
                                                                  new FacetValue
                                                                      {
                                                                          FacetId = 2,
                                                                          Value = "Yes"
                                                                      },
                                                                  new FacetValue
                                                                      {
                                                                          FacetId = 2,
                                                                          Value = "No"
                                                                      }
                                                              }
                                             };

        [Test]
        public void All_ShouldReturnAllTheFacets_WhenThereAreFacetsToReturn()
        {
            var facetRepository = Substitute.For<IRepository<Facet>>();
            facetRepository.GetAll().Returns(new [] { favouriteAnimal, bigFeet });

            var facetsService = new FacetsService(facetRepository);

            var allFacets = facetsService.All();

            CollectionAssert.AreEqual(new[] { favouriteAnimal, bigFeet }, allFacets);
        }

        [Test]
        public void All_ShouldReturnAnEmptyList_WhenThereAreNoFacetsToReturn()
        {
            var facetRepository = Substitute.For<IRepository<Facet>>();
            facetRepository.GetAll().Returns(Enumerable.Empty<Facet>());

            var facetsService = new FacetsService(facetRepository);

            var allFacets = facetsService.All();

            CollectionAssert.AreEqual(Enumerable.Empty<Facet>(), allFacets);
        }

        [Test]
        public void Get_ShouldReturnTheSpecifiedFacet_WhenTheFacetExists()
        {
            const int facetId = 2;
            
            var facetRepository = Substitute.For<IRepository<Facet>>();
            facetRepository.GetById(facetId).Returns(bigFeet);

            var facetsService = new FacetsService(facetRepository);

            var returnedFacet = facetsService.Get(facetId);

            Assert.AreEqual(bigFeet, returnedFacet);
        }

        [Test]
        public void Get_ShouldReturnNull_WhenTheSpecifiedFacetDoesNotExist()
        {
            const int facetId = 3;
            
            var facetRepository = Substitute.For<IRepository<Facet>>();
            facetRepository.GetById(facetId).Returns((Facet)null);

            var facetsService = new FacetsService(facetRepository);

            var returnedFacet = facetsService.Get(facetId);

            Assert.IsNull(returnedFacet);
        }
    }
}
