using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.DummyDataGenerator.Generation
{
    using GiveCRM.DummyDataGenerator.Data;

    internal class MemberGenerator
    {
        private readonly RandomSource random = new RandomSource();
        private readonly List<string> generatedEmails = new List<string>(); 
        private readonly List<string> generatedPostalAddresses = new List<string>();

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
            string firstName = isFemale ? RandomFemaleFirstName() : RandomMaleFirstName();
            TitleData titleSalutation = MakeTitleSalutation(isFemale);

            var newMember = new Member
                {
                    FirstName = firstName,
                    LastName = random.PickFromList(NameData.Surnames),
                    Title = titleSalutation.Title,
                    Salutation = titleSalutation.Salutation,
                    Country = "United Kingdom"
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
                        Type = "Home",
                        Number = random.PhoneDigits()
                    });
            }

            if (random.Percent(60))
            {
                member.PhoneNumbers.Add(new PhoneNumber
                {
                    Type = "Work",
                    Number = random.PhoneDigits()
                });
            }

            if (random.Percent(60))
            {
                member.PhoneNumbers.Add(new PhoneNumber
                {
                    Type = "Mobile",
                    Number = random.PhoneDigits()
                });
            }
        }

        private void MakeStreetAddress(Member member)
        {
            string streetAddress = GenerateStreetAddress();
            while (generatedPostalAddresses.Contains(streetAddress))
            {
                // duplicate! try again
                streetAddress = GenerateStreetAddress();
            }

            member.AddressLine1 = streetAddress;

            member.PostalCode = RandomPostalCode();
            member.City = random.PickFromList(AddressData.Cities);
        }

        private string GenerateStreetAddress()
        {
            string street = random.PickFromList(AddressData.StreetNames);
            string streetNumber = (random.Next(100) + 1).ToString();

            if (random.Percent(10))
            {
                streetNumber += random.Bool() ? "A" : "B";
            }

            return streetNumber + " " + street;
        }

        private string RandomPostalCode()
        {
            return random.PickFromList(AddressData.PostCodes) + random.Next(10) + " " +
                random.Next(10) + random.Letter() + random.Letter();
        }

        private void MakeEmailAddress(Member member)
        {
            string newEmailAddress = GenerateEmailAddress(member);
            while (generatedEmails.Contains(newEmailAddress))
            {
                // duplicate! regenerate it.
               newEmailAddress = GenerateEmailAddress(member);
            }
            member.EmailAddress = newEmailAddress;
        }

        private string GenerateEmailAddress(Member member)
        {
            string sep = random.PickFromList(NameData.EmailSeparators);

            if (random.Percent(30))
            {
                sep += random.Letter() + random.PickFromList(NameData.EmailSeparators);
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

            string domain = random.PickFromList(NameData.EmailDomains);

            return name + "@" + domain;
        }

        private TitleData MakeTitleSalutation(bool isFemale)
        {
            return isFemale ? random.PickFromList(NameData.FemaleTitles) : random.PickFromList(NameData.MaleTitles);
        }

        private string RandomMaleFirstName()
        {
            return random.PickFromList(NameData.MaleFirstNames);
        }

        private string RandomFemaleFirstName()
        {
            return random.PickFromList(NameData.FemaleFirstNames);
        }
    }
}