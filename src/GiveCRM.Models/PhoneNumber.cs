namespace GiveCRM.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public PhoneNumberType PhoneNumberType { get; set; }
        public string Number { get; set; }

        public override string ToString()
        {
            return PhoneNumberType + " " + Number;
        }
    }
}