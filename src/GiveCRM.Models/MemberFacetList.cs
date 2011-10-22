using System.Linq;
using System.Collections.Generic;

namespace GiveCRM.Models
{
    public class MemberFacetList : MemberFacet
    {
        public ICollection<FacetValue> Values { get; set; }

        public override string ToString()
        {
            return string.Join(",", Values);
        }
    }
}