namespace GiveCRM.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
    }
}