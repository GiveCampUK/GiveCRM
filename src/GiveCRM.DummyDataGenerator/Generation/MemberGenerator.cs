using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.DummyDataGenerator.Generation
{
    using GiveCRM.DummyDataGenerator.Data;

    internal class MemberGenerator
    {
        private readonly RandomSource random = new RandomSource();

        // these are used to check for and avoid generating duplicate values
        // use dictionary not list since contains checking is faster
        private readonly Dictionary<string, bool> generatedEmails = new Dictionary<string, bool>();
        private readonly Dictionary<string, bool> generatedPostalAddresses = new Dictionary<string, bool>();

        private int lastReferenceIndex = 0;

        public List<Member> Generate(int count)
        {
            List<Member> result = new List<Member>(count);

            for (int i = 0; i < count; i++)
            {
                result.Add(GenerateMember());
            }

            return result;
        }

        private Member GenerateMember()
        {
            bool isFemale = random.Bool();
            string familyName = random.PickFromList(FamilyNames.Data);
            // make first name different from Family name, ie.g. no "Scott Scott" or "Major Major"
            string firstName;
            do
            {
                firstName = isFemale ? RandomFemaleFirstName() : RandomMaleFirstName();
            }
            while(firstName == familyName);

            TitleDataItem titleSalutation = MakeTitleSalutation(isFemale);

            var newMember = new Member
                {
                    FirstName = firstName,
                    LastName = familyName,
                    Title = titleSalutation.Title,
                    Salutation = titleSalutation.Salutation,
                };

            newMember.Reference = this.NextReference(newMember);
            MakeEmailAddress(newMember);
            MakeStreetAddress(newMember);

            MakePhoneNumbers(newMember);

            return newMember;
        }

        private string NextReference(Member member)
        {
            lastReferenceIndex++;

            string namePrefix = member.FirstName.Substring(0, 1) + member.LastName.Substring(0, 1);
            return namePrefix + lastReferenceIndex.ToString("000000");

        }

        private void MakePhoneNumbers(Member member)
        {
            member.PhoneNumbers = new List<PhoneNumber>();
            if (random.Percent(10))
            {
                // 10% have no phone numbers
                return;
            }

            if(random.Percent(60))
            {
                member.PhoneNumbers.Add(new PhoneNumber
                    {
                        PhoneNumberType = PhoneNumberType.Home,
                        Number = random.PhoneDigits()
                    });
            }

            if (random.Percent(60))
            {
                member.PhoneNumbers.Add(new PhoneNumber
                {
                    PhoneNumberType = PhoneNumberType.Work,
                    Number = random.PhoneDigits()
                });
            }

            if (random.Percent(60))
            {
                member.PhoneNumbers.Add(new PhoneNumber
                {
                    PhoneNumberType = PhoneNumberType.Mobile,
                    Number = random.PhoneDigits()
                });
            }
        }

        private void MakeStreetAddress(Member member)
        {
            string streetAddress = GenerateStreetAddress();
            while (generatedPostalAddresses.ContainsKey(streetAddress))
            {
                // duplicate! try again
                streetAddress = GenerateStreetAddress();
            }

            generatedPostalAddresses.Add(streetAddress, true);
            member.AddressLine1 = streetAddress;

            TownDataItem townData = random.PickFromList(TownData.Data);
            string postCodePrefix = random.PickFromList(townData.PostalCodePrefixes);

            member.PostalCode = RandomPostalCode(postCodePrefix);
            member.City = townData.Town;
            member.Region = townData.Region;
            member.Country = townData.Country;
        }

        private string GenerateStreetAddress()
        {
            string street = random.PickFromList(AddressData.StreetNamePrefix) + " " 
                + random.PickFromList(AddressData.StreetNames) + " " + random.PickFromList(AddressData.StreetSuffix);
            street = street.Trim();
            string streetNumber = (random.Next(200) + 1).ToString();

            if (random.Percent(20))
            {
                streetNumber += random.Bool() ? "A" : "B";
            }

            return streetNumber + " " + street;
        }

        private string RandomPostalCode(string prefix)
        {
            return prefix + random.Next(10) + " " +
                random.Next(10) + random.Letter() + random.Letter();
        }

        private void MakeEmailAddress(Member member)
        {
            string newEmailAddress = GenerateEmailAddress(member);
            while (generatedEmails.ContainsKey(newEmailAddress))
            {
                // duplicate! regenerate it.
               newEmailAddress = GenerateEmailAddress(member);
            }
            generatedEmails.Add(newEmailAddress, true);

            member.EmailAddress = newEmailAddress;
        }

        private string GenerateEmailAddress(Member member)
        {
            string sep = random.PickFromList(EmailData.Separators);

            if (random.Percent(30))
            {
                sep += random.Letter() + random.PickFromList(EmailData.Separators);
            }

            string name;

            if (random.Percent(30))
            {
                name = member.LastName + sep + member.FirstName;
            }
            else
            {
                name = member.FirstName + sep + member.LastName;
            }

            if (random.Bool())
            {
                name += this.random.Next(100).ToString();
            }

            string domain = random.PickFromList(EmailData.Domains);

            return name + "@" + domain;
        }

        private TitleDataItem MakeTitleSalutation(bool isFemale)
        {
            return isFemale ? random.PickFromList(TitleData.FemaleTitles) : random.PickFromList(TitleData.MaleTitles);
        }

        private string RandomMaleFirstName()
        {
            return random.PickFromList(MaleNames.Data);
        }

        private string RandomFemaleFirstName()
        {
            return random.PickFromList(FemaleNames.Data);
        }
    }
}