using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.DataAccess
{
    internal class MemberFacetAggregator
    {
        private readonly IEnumerable<dynamic> _rawFacets;
        private readonly Dictionary<int, MemberFacet> _memberFacets = new Dictionary<int, MemberFacet>(); 

        public MemberFacetAggregator(IEnumerable<dynamic> rawFacets)
        {
            _rawFacets = rawFacets;
        }

        public IEnumerable<MemberFacet> Run()
        {
            foreach (var facet in _rawFacets)
            {
                if (!string.IsNullOrWhiteSpace(facet.FreeTextValue))
                {
                    AddFreeTextFacet(facet);
                }
                else
                {
                    AddListFacetValue(facet);
                }
            }

            return _memberFacets.Values;
        }

        private void AddListFacetValue(dynamic facet)
        {
            MemberFacetList facetList;

            if (!_memberFacets.ContainsKey(facet.FacetId))
            {
                facetList = CreateMemberFacetList(facet);
            }
            else
            {
                facetList = _memberFacets[facet.FacetId];
            }
            facetList.Values.Add(new MemberFacetValue {Id = facet.Id, Value = facet.Value});
        }

        private MemberFacetList CreateMemberFacetList(dynamic facet)
        {
            var facetList = new MemberFacetList
                                {
                                    Id = facet.Id,
                                    FacetId = facet.FacetId,
                                    MemberId = facet.MemberId,
                                    Values = new List<FacetValue>(),
                                    Facet = new Facet { Id = facet.FacetId, Type = facet.Type, Name = facet.Name }
                                };
            _memberFacets.Add(facet.FacetId, facetList);
            return facetList;
        }

        private void AddFreeTextFacet(dynamic facet)
        {
            _memberFacets.Add(facet.FacetId,
                              new MemberFacetFreeText
                                  {
                                      Id = facet.Id,
                                      FacetId = facet.FacetId,
                                      MemberId = facet.MemberId,
                                      FreeTextValue = facet.FreeTextValue,
                                      Facet = new Facet { Id = facet.FacetId, Type = facet.Type, Name = facet.Name }
                                  });
        }
    }
}