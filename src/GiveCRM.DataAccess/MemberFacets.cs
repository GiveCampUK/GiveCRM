using System.Collections.Generic;

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
                        _db.MemberFacets.FacetValueId, _db.MemberFacets.FacetValue.Value);

            return new MemberFacetAggregator(query).Run();
        }
    }
}
