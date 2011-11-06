using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveCRM.Models;
using GiveCRM.Models.Search;
using GiveCRM.Web.Models.Search;
using Simple.Data;

namespace GiveCRM.DataAccess
{
    public class Search
    {
        private readonly dynamic _db = Database.OpenNamedConnection("GiveCRM");

        public IEnumerable<SearchCriteria> GetEmptySearchCriteria(IFacets facets = null)
        {
            if (facets == null) facets = new Facets();

            yield return new LocationSearchCriteria { InternalName = LocationSearchCriteria.City, DisplayName = "City", Type = SearchFieldType.String };
            yield return new LocationSearchCriteria { InternalName = LocationSearchCriteria.Region, DisplayName = "Region", Type = SearchFieldType.String };
            yield return new LocationSearchCriteria { InternalName = LocationSearchCriteria.PostalCode, DisplayName = "Postal code", Type = SearchFieldType.String };

            yield return new DonationSearchCriteria { InternalName = DonationSearchCriteria.IndividualDonation, DisplayName = "Individual donation", Type = SearchFieldType.Double };
            yield return new DonationSearchCriteria { InternalName = DonationSearchCriteria.TotalDonations, DisplayName = "Total donations", Type = SearchFieldType.Double };
            yield return new DonationSearchCriteria { InternalName = DonationSearchCriteria.LastDonationDate, DisplayName = "Last donation date", Type = SearchFieldType.Date };

            yield return new CampaignSearchCriteria { InternalName = CampaignSearchCriteria.DonatedToCampaign, DisplayName = "Donated to campaign", Type = SearchFieldType.String };

            foreach (Facet f in facets.AllFreeText())
            {
                yield return new FacetSearchCriteria { InternalName = "freeTextFacet_" + f.Id, DisplayName = f.Name, Type = SearchFieldType.String, FacetId = f.Id };
            }
        }

        public IEnumerable<int> RunWithIdOnly(IEnumerable<SearchCriteria> criteria)
        {
            var criteriaList = criteria.ToList();

            if (criteriaList.Count == 0)
            {
                // don't attempt to search if there are not criteria - don't want the whole database
                return Enumerable.Empty<int>();
            }

            var query = CompileQuery(criteriaList);

            return query.Select(_db.Members.Id).ToScalarList<int>();
        }
    }
}
