namespace GiveCRM.BusinessLogic.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using GiveCRM.Models;
    using GiveCRM.Models.Search;
    using GiveCRM.Web.Models.Search;
    using NSubstitute;
    using NUnit.Framework;

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
            var actualEmptySearchCriteria = searchService.GetEmptySearchCriteria();

            CollectionAssert.AreEqual(expectedEmptySearchCriteria.ToArray(), actualEmptySearchCriteria.ToArray(), new SearchCriteriaComparer());
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

        private class SearchCriteriaComparer : IComparer, IComparer<SearchCriteria>
        {
            #region Implementation of IComparer<in SearchCriteria>

            /// <summary>
            /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <returns>
            /// A signed integer that indicates the relative values of <paramref name="x"/> and <paramref name="y"/>, as shown in the following table.Value Meaning Less than zero<paramref name="x"/> is less than <paramref name="y"/>.Zero<paramref name="x"/> equals <paramref name="y"/>.Greater than zero<paramref name="x"/> is greater than <paramref name="y"/>.
            /// </returns>
            /// <param name="x">The first object to compare.</param><param name="y">The second object to compare.</param>
            public int Compare(SearchCriteria x, SearchCriteria y)
            {
                int internalNameComparison = Comparer<string>.Default.Compare(x.InternalName, y.InternalName);
                if (internalNameComparison != 0)
                {
                    return internalNameComparison;
                }

                int displayNameComparison = Comparer<string>.Default.Compare(x.DisplayName, y.DisplayName);
                if (displayNameComparison != 0)
                {
                    return displayNameComparison;
                }

                return Comparer<SearchFieldType>.Default.Compare(x.Type, y.Type);
            }

            #endregion

            #region Implementation of IComparer

            /// <summary>
            /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <returns>
            /// A signed integer that indicates the relative values of <paramref name="x"/> and <paramref name="y"/>, as shown in the following table.Value Meaning Less than zero <paramref name="x"/> is less than <paramref name="y"/>. Zero <paramref name="x"/> equals <paramref name="y"/>. Greater than zero <paramref name="x"/> is greater than <paramref name="y"/>. 
            /// </returns>
            /// <param name="x">The first object to compare. </param><param name="y">The second object to compare. </param><exception cref="T:System.ArgumentException">Neither <paramref name="x"/> nor <paramref name="y"/> implements the <see cref="T:System.IComparable"/> interface.-or- <paramref name="x"/> and <paramref name="y"/> are of different types and neither one can handle comparisons with the other. </exception><filterpriority>2</filterpriority>
            public int Compare(object x, object y)
            {
                var xSearchCriteria = x as SearchCriteria;
                var ySearchCriteria = y as SearchCriteria;

                if (xSearchCriteria == null && ySearchCriteria != null)
                {
                    return -1;
                }

                if (xSearchCriteria != null && ySearchCriteria == null)
                {
                    return 1;
                }

                return Compare(xSearchCriteria, ySearchCriteria);
            }

            #endregion
        }
    }
}
