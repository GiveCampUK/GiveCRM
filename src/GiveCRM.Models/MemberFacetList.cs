using System.Collections.Generic;

namespace GiveCRM.Models
{
    public class MemberFacetList : MemberFacet
    {
        public ICollection<FacetValue> Values { get; set; }
    }
}