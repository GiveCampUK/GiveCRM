using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GiveCRM.Models;

namespace GiveCRM.Web.Models.Member
{
    public class MemberSearchViewModel
    {
        public IEnumerable<GiveCRM.Models.Member> Results { get; set; }
        public bool AreMore { get; set; }
    }
}