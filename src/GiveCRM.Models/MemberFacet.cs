namespace GiveCRM.Models
{
    public abstract class MemberFacet
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int FacetId { get; set; }
        public Facet Facet { get; set; }

        public override string ToString()
        {
            return Id + " " + MemberId + FacetId;
        }
    }
}
