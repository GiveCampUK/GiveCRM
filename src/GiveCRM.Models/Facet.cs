namespace GiveCRM.Models
{
    using System.Collections.Generic;

    public class Facet
    {
        public int Id { get; set; }
        public FacetType Type { get; set; }
        public string Name { get; set; }
        public ICollection<FacetValue> Values { get; set; }

        public override string ToString()
        {
            return Id + " " + Type + " " + Name;
        }
    }
}
