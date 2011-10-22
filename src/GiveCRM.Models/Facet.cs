using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveCRM.Models
{
    public class Facet
    {
        public int Id { get; set; }
        public FacetType Type { get; set; }
        public string Name { get; set; }
        public ICollection<FacetValue> Values { get; set; }
    }
}
