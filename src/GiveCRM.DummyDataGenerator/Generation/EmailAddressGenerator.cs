using System.Collections.Generic;
using GiveCRM.DummyDataGenerator.Data;

namespace GiveCRM.DummyDataGenerator.Generation
{
    internal class EmailAddressGenerator
    {
        private readonly RandomSource random = new RandomSource();

        // this is used to check for and avoid generating duplicate values
        // use dictionary not list since contains checking is faster
        private readonly Dictionary<string, bool> generatedEmails = new Dictionary<string, bool>();

        internal string GenerateEmailAddress(string firstName, string lastName)
        {
            if (random.Percent(5))
            {
                // 5% of people haven't supplied an email address
                return null;
            }

            string emailAddress;

            do
            {
                emailAddress = MakeEmailAddress(firstName, lastName);
            }
            while (generatedEmails.ContainsKey(emailAddress));
            
            generatedEmails.Add(emailAddress, true);
            return emailAddress;
        }

        private string MakeEmailAddress(string firstName, string lastName)
        {
            string sep = random.PickFromList(EmailData.Separators);

            if (random.Percent(30))
            {
                sep += random.Letter() + random.PickFromList(EmailData.Separators);
            }

            string name = random.Percent(30) ? lastName + sep + firstName : firstName + sep + lastName;

            if (random.Bool())
            {
                name += this.random.Next(100).ToString();
            }

            string domain = random.PickFromList(EmailData.Domains);
            return name + "@" + domain;
        }         
    }
}