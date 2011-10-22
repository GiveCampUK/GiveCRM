using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveCRM.Models;
using Simple.Data;

namespace GiveCRM.DataAccess
{
    public class MemberFacets
    {
        private readonly dynamic _db = Database.OpenNamedConnection("GiveCRM");

        public IEnumerable<MemberFacet> ForMember(int memberId)
        {
            var query = _db.MemberFacets.FindAllByMemberId(memberId)
                .Select(_db.MemberFacets.ID, _db.MemberFacets.FacetId, _db.MemberFacets.FreeTextValue,
                        _db.MemberFacets.MemberFacetValue.FacetValueId,
                        _db.MemberFacets.MemberFacetValue.FacetValue.Value,
                        _db.MemberFacets.MemberFacetValue.Id.As("MemberFacetValueId"),
                        _db.MemberFacets.Facet.Type, _db.MemberFacets.Facet.Name);

            throw new NotImplementedException();

            //return new MemberFacetAggregator(query).Run();
        }

        public MemberFacet Insert(MemberFacet facet)
        {
            var freeTextFacet = facet as MemberFacetFreeText;
            if (freeTextFacet != null)
            {
                return Insert(freeTextFacet);
            }

            var listFacet = facet as MemberFacetList;
            if (listFacet != null)
            {
                return Insert(listFacet);
            }

            throw new InvalidOperationException("Unknown MemberFacet type.");
        }

        public MemberFacetFreeText Insert(MemberFacetFreeText facet)
        {
            return _db.MemberFacets.Insert(facet);
        }

        public MemberFacetList Insert(MemberFacetList facet)
        {
            MemberFacetList inserted = _db.MemberFacets.Insert(facet);
            inserted.Values = new List<MemberFacetValue>();
            foreach (var value in facet.Values)
            {
                value.MemberFacetId = inserted.Id;
                MemberFacetValue insertedValue = _db.MemberFacetValues.Insert(value);
                inserted.Values.Add(insertedValue);
            }
            return inserted;
        }
    }
}
