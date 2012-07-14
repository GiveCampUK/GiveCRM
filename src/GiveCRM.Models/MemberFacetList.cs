namespace GiveCRM.Models
{
    using System.Collections.Generic;

    public class MemberFacetList : MemberFacet
    {
        public ICollection<int> Ids { get; set; } 
        public ICollection<MemberFacetValue> Values { get; set; }

        public override string ToString()
        {
            return string.Join(",", Values);
        }
    }

    public class MemberFacetValue
    {
        public int Id { get; set; }
        public int FacetValueId { get; set; }

        public int MemberFacetId { get; set; }
    }
}