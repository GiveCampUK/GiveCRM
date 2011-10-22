namespace GiveCRM.Models
{
    public class MemberFacetFreeText : MemberFacet
    {
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}