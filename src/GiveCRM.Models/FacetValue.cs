namespace GiveCRM.Models
{
    public class FacetValue
    {
        public int Id { get; set; }
        public int FacetId { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return FacetId + " " + Id + " " + Value;
        }
    }
}