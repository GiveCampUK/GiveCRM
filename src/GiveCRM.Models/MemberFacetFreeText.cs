namespace GiveCRM.Models
{
    public class MemberFacetFreeText : MemberFacet
    {
        public string FreeTextValue { get; set; }

        public override string ToString()
        {
            return FreeTextValue;
        }
    }
}