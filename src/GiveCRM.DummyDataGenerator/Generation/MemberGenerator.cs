namespace GiveCRM.DummyDataGenerator.Generation
{
    using System;
    using System.Collections.Generic;
    using GiveCRM.DataAccess;
    using GiveCRM.DummyDataGenerator.Data;
    using GiveCRM.Models;

    internal class MemberGenerator : BaseGenerator
    {
        internal override string GeneratedItemType{get {return "members";}}

        private readonly RandomSource random = new RandomSource();
        private readonly TitleGenerator titleGenerator = new TitleGenerator();
        private readonly EmailAddressGenerator emailGenerator = new EmailAddressGenerator();
        private readonly AddressGenerator addressGenerator = new AddressGenerator();

        private int lastReferenceNumber;

        internal MemberGenerator(Action<string> logAction) : base(logAction)
        {}

        internal override void Generate(int numberToGenerate)
        {
            Members membersDb = new Members(new SingleTenantDatabaseProvider());
            GenerateMultiple(numberToGenerate, () =>
                                                   {
                                                       var member = GenerateMember();
                                                       membersDb.Insert(member);
                                                   });
        }

        private Member GenerateMember()
        {
            bool isFemale = random.Bool();
            string familyName = random.PickFromList(FamilyNames.Data);

            // make first name different from Family name, ie.g. no "Scott Scott" or "Major Major"
            string firstName;
            
            do
            {
                var namesList = isFemale ? FemaleNames.Data : MaleNames.Data;
                firstName = random.PickFromList(namesList);
            }
            while (firstName == familyName);

            TitleDataItem titleSalutation = isFemale ? titleGenerator.GenerateFemaleTitle() : titleGenerator.GenerateMaleTitle();

            var member = new Member
                                {
                                            FirstName = firstName,
                                            LastName = familyName,
                                            Title = titleSalutation.Title,
                                            Salutation = titleSalutation.Salutation,
                                };

            member.Reference = this.NextReference(member);
            member.EmailAddress = emailGenerator.GenerateEmailAddress(member.FirstName, member.LastName);

            var addressData = addressGenerator.GenerateStreetAddress();
            member.AddressLine1 = addressData.Address;
            member.City = addressData.City;
            member.Region = addressData.Region;
            member.PostalCode = addressData.PostalCode;
            member.Country = addressData.Country;

            MakePhoneNumbers(member);
            return member;
        }

        private string NextReference(Member member)
        {
            lastReferenceNumber++;
            string namePrefix = member.FirstName.Substring(0, 1) + member.LastName.Substring(0, 1);
            return namePrefix + lastReferenceNumber.ToString("000000");
        }

        private void MakePhoneNumbers(Member member)
        {
            member.PhoneNumbers = new List<PhoneNumber>();

            if (random.Percent(10))
            {
                // 10% have no phone numbers
                return;
            }

            if (random.Percent(60))
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
    }
}
