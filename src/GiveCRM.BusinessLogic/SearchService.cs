using System;
using System.Collections.Generic;
using GiveCRM.Models;
using GiveCRM.Models.Search;
using GiveCRM.Web.Models.Search;

namespace GiveCRM.BusinessLogic
{
    public class SearchService : ISearchService
    {
        private readonly IFacetRepository facetRepository;

        public SearchService(IFacetRepository facetRepository)
        {
            if (facetRepository == null)
            {
                throw new ArgumentNullException("facetRepository");
            }

            this.facetRepository = facetRepository;
        }

        public IEnumerable<SearchCriteria> GetEmptySearchCriteria()
        {
            yield return new LocationSearchCriteria { InternalName = LocationSearchCriteria.City, DisplayName = "City", Type = SearchFieldType.String };
            yield return new LocationSearchCriteria { InternalName = LocationSearchCriteria.Region, DisplayName = "Region", Type = SearchFieldType.String };
            yield return new LocationSearchCriteria { InternalName = LocationSearchCriteria.PostalCode, DisplayName = "Postal code", Type = SearchFieldType.String };

            yield return new DonationSearchCriteria { InternalName = DonationSearchCriteria.IndividualDonation, DisplayName = "Individual donation", Type = SearchFieldType.Double };
            yield return new DonationSearchCriteria { InternalName = DonationSearchCriteria.TotalDonations, DisplayName = "Total donations", Type = SearchFieldType.Double };
            yield return new DonationSearchCriteria { InternalName = DonationSearchCriteria.LastDonationDate, DisplayName = "Last donation date", Type = SearchFieldType.Date };

            yield return new CampaignSearchCriteria { InternalName = CampaignSearchCriteria.DonatedToCampaign, DisplayName = "Donated to campaign", Type = SearchFieldType.String };

            foreach (Facet f in facetRepository.GetAllFreeText())
            {
                yield return new FacetSearchCriteria { InternalName = "freeTextFacet_" + f.Id, DisplayName = f.Name, Type = SearchFieldType.String, FacetId = f.Id };
            }
        }
    }
}