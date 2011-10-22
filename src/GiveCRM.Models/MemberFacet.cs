using System;
using System.Linq;
using System.Text;

namespace GiveCRM.Models
{
    public class MemberFacet
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int FacetId { get; set; }
    }
}
