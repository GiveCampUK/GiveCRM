namespace GiveCRM.Models
{
    public class PhoneNumber
    {
        public int ID { get; set; }
        public int MemberID { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
    }
}