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

        private dynamic CompileQuery(List<SearchCriteria> criteriaList)
        {
            var expr = CompileLocationCriteria(criteriaList.OfType<LocationSearchCriteria>(), null);
            SimpleExpression having = null;
            CompileDonationCriteria(criteriaList.OfType<DonationSearchCriteria>(), ref expr, ref having);

            var query = _db.Members.All();

            if (expr != null)
            {
                query = query.Where(expr);
            }

            if (having != null)
            {
                query = query.Having(having);
            }
            return query;
        }

        internal SimpleExpression CompileFacetCriteria(IEnumerable<FacetSearchCriteria> criteria, SimpleExpression expr)
        {
            foreach (var criterion in criteria)
            {
                var facetReference = _db.Member.MemberFacet.As(criterion.InternalName);
                var facetExpr = facetReference.FacetId == criterion.FacetId &&
                                CompileStringExpression(facetReference.FreeTextValue, criterion);
                expr = Combine(expr, facetExpr);
            }

            return expr;
        }

        internal SimpleExpression CompileCampaignCriteria(IEnumerable<CampaignSearchCriteria> criteria, SimpleExpression expr)
        {
            var criterion = criteria.FirstOrDefault();
            if (criterion == null) return expr;
            return Combine(expr, CompileStringExpression(_db.Members.Campaign.Name, criterion));
        }

        internal void CompileDonationCriteria(IEnumerable<DonationSearchCriteria> criteria, ref SimpleExpression expr, ref SimpleExpression having)
        {
            foreach (var donationCriteria in criteria)
            {
                switch (donationCriteria.InternalName)
                {
                    case DonationSearchCriteria.IndividualDonation:
                        expr = Combine(expr, CompileIndividualDonationCriteria(donationCriteria));
                        break;
                    case DonationSearchCriteria.LastDonationDate:
                        having = Combine(having, CompileLastDonationDate(donationCriteria));
                        break;
                    case DonationSearchCriteria.TotalDonations:
                        having = Combine(having, CompileTotalDonationCriteria(donationCriteria));
                        break;
                    default:
                        throw new NotSupportedException("Criteria type not supported");
                }
            }
        }

        private SimpleExpression CompileIndividualDonationCriteria(DonationSearchCriteria donationCriteria)
        {
            var column = _db.Members.Donations.Amount;
            decimal amount;
            if (!decimal.TryParse(donationCriteria.Value, out amount)) throw new InvalidOperationException("Cannot convert value to decimal.");
            return CompileExpression(donationCriteria, amount, column);
        }

        private SimpleExpression CompileTotalDonationCriteria(DonationSearchCriteria donationCriteria)
        {
            var column = _db.Members.Donations.Amount.Total();
            decimal amount;
            if (!decimal.TryParse(donationCriteria.Value, out amount)) throw new InvalidOperationException("Cannot convert value to decimal.");
            return CompileExpression(donationCriteria, amount, column);
        }

        private SimpleExpression CompileLastDonationDate(DonationSearchCriteria donationCriteria)
        {
            var column = _db.Members.Donations.Date.Max();
            DateTime date;
            if (!DateTime.TryParse(donationCriteria.Value, out date)) throw new InvalidOperationException("Cannot convert value to date.");
            return CompileExpression(donationCriteria, date, column);
        }

        private static SimpleExpression CompileExpression<T>(DonationSearchCriteria donationCriteria, T amount, dynamic column)
            where T : IComparable<T>
        {
            switch (donationCriteria.SearchOperator)
            {
                case SearchOperator.EqualTo:
                    return column == amount;
                case SearchOperator.NotEqualTo:
                    return column != amount;
                case SearchOperator.GreaterThan:
                    return column > amount;
                case SearchOperator.GreaterThanOrEqualTo:
                    return column >= amount;
                case SearchOperator.LessThan:
                    return column < amount;
                case SearchOperator.LessThanOrEqualTo:
                    return column <= amount;
                default:
                    throw new InvalidOperationException("Operator not valid with decimal value.");
            }
        }

        internal SimpleExpression CompileLocationCriteria(IEnumerable<LocationSearchCriteria> criteria, SimpleExpression current)
        {
            foreach (var locationCriteria in criteria)
            {
                var column = _db.Members[locationCriteria.InternalName];
                var expr = CompileStringExpression(column, locationCriteria);

                current = Combine(current, expr);
            }

            return current;
        }

        private static SimpleExpression CompileStringExpression(dynamic column, SearchCriteria criteria)
        {
            SimpleExpression expr;
            switch (criteria.SearchOperator)
            {
                case SearchOperator.EqualTo:
                    expr = column == criteria.Value;
                    break;
                case SearchOperator.NotEqualTo:
                    expr = column != criteria.Value;
                    break;
                case SearchOperator.StartsWith:
                    expr = column.Like(criteria.Value + "%");
                    break;
                case SearchOperator.EndsWith:
                    expr = column.Like("%" + criteria.Value);
                    break;
                case SearchOperator.Contains:
                    expr = column.Like("%" + criteria.Value + "%");
                    break;
                default:
                    throw new InvalidOperationException("Operator not valid with String value.");
            }
            return expr;
        }

        private static SimpleExpression Combine(SimpleExpression first, SimpleExpression second)
        {
            if (ReferenceEquals(first, null)) return second;
            if (ReferenceEquals(second, null)) return first;
            return first && second;
        }
    }
}
