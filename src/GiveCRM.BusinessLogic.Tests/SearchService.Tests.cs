using System;
using System.Collections.Generic;
using System.Linq;
using GiveCRM.Models;
using GiveCRM.Models.Search;
using GiveCRM.Web.Models.Search;
using NSubstitute;
using NUnit.Framework;

namespace GiveCRM.BusinessLogic.Tests
{
    [TestFixture]
    public class SearchServiceTests
    {
        private readonly Facet bigFeetFacet = new Facet
                                                         {
                                                             Id = 1,
                                                             Name = "Has Big Feet",
                                                             Type = FacetType.FreeText
                                                         };

        private readonly Facet smellyFeetFacet = new Facet
                                                            {
                                                                Id = 2,
                                                                Name = "Has Smelly Feet",
                                                                Type = FacetType.FreeText
                                                            };

        [Test]
        public void GetEmptySearchCriteria_ShouldReturnAListOfSearchCriteria()
        {
            var facetRepository = GetFacetRepository();
            var expectedEmptySearchCriteria = GetExpectedEmptySearchCriteria(new[] { bigFeetFacet, smellyFeetFacet});

            var searchService = new SearchService(facetRepository);
            var actualEmptySearchCriteria = searchService.GetEmptySearchCriteria().ToArray();

            CollectionAssert.AreEqual(expectedEmptySearchCriteria, actualEmptySearchCriteria);
        }

        private static IEnumerable<SearchCriteria> GetExpectedEmptySearchCriteria(IEnumerable<Facet> freeTextFacets)
        {
            var locationSearchCriteria = new[]
                                             {
                                                 new LocationSearchCriteria
                                                     {
                                                         InternalName = LocationSearchCriteria.City,
                                                         DisplayName = "City",
                                                         Type = SearchFieldType.String
                                                     },
                                                 new LocationSearchCriteria
                                                     {
                                                         InternalName = LocationSearchCriteria.Region,
                                                         DisplayName = "Region",
                                                         Type = SearchFieldType.String
                                                     },
                                                 new LocationSearchCriteria
                                                     {
                                                         InternalName = LocationSearchCriteria.PostalCode,
                                                         DisplayName = "Postal code",
                                                         Type = SearchFieldType.String
                                                     }
                                             };

            var donationSearchCriteria = new[]
                                             {

                                                 new DonationSearchCriteria
                                                     {
                                                         InternalName = DonationSearchCriteria.IndividualDonation,
                                                         DisplayName = "Individual donation",
                                                         Type = SearchFieldType.Double
                                                     },
                                                 new DonationSearchCriteria
                                                     {
                                                         InternalName = DonationSearchCriteria.TotalDonations,
                                                         DisplayName = "Total donations",
                                                         Type = SearchFieldType.Double
                                                     },
                                                 new DonationSearchCriteria
                                                     {
                                                         InternalName = DonationSearchCriteria.LastDonationDate,
                                                         DisplayName = "Last donation date",
                                                         Type = SearchFieldType.Date
                                                     }
                                             };

            var campaignSearchCriteria = new[]
                                             {
                                                 new CampaignSearchCriteria
                                                     {
                                                         InternalName = CampaignSearchCriteria.DonatedToCampaign,
                                                         DisplayName = "Donated to campaign",
                                                         Type = SearchFieldType.String
                                                     }
                                             };

            var facetSearchCriteria = freeTextFacets.Select(f => new FacetSearchCriteria
                                                                     {
                                                                         InternalName = "freeTextFacet_" + f.Id,
                                                                         DisplayName = f.Name,
                                                                         Type = SearchFieldType.String,
                                                                         FacetId = f.Id
                                                                     });

            return locationSearchCriteria.Cast<SearchCriteria>()
                                         .Union(donationSearchCriteria)
                                         .Union(campaignSearchCriteria)
                                         .Union(facetSearchCriteria);
        }

        private IFacetRepository GetFacetRepository()
        {
            var facetRepository = Substitute.For<IFacetRepository>();
            facetRepository.GetAllFreeText().Returns(new[] { bigFeetFacet, smellyFeetFacet });

            return facetRepository;
        }
    }
}
