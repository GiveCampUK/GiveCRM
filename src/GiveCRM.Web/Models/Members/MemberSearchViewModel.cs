using System.Collections.Generic;

namespace GiveCRM.Web.Models.Members
{
    public class MemberSearchViewModel
    {
        public IEnumerable<GiveCRM.Models.Member> Results { get; set; }
        public bool AreMore { get; set; }
    }
}