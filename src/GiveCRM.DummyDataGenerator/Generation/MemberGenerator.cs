using System;
using System.Collections.Generic;
using GiveCRM.DataAccess;
using GiveCRM.DummyDataGenerator.Data;
using GiveCRM.Models;

namespace GiveCRM.DummyDataGenerator.Generation
{
    internal class MemberGenerator
    {
        private readonly RandomSource random = new RandomSource();
        private readonly TitleGenerator titleGenerator = new TitleGenerator();
        private readonly EmailAddressGenerator emailGenerator = new EmailAddressGenerator();
        private readonly AddressGenerator addressGenerator = new AddressGenerator();

        private readonly Action<string> logAction;

        private int lastReferenceNumber;

        public MemberGenerator(Action<string> logAction)
        {
            this.logAction = logAction;
        }

        public void GenerateMembers(int numberToGenerate)
        {
            logAction(string.Format("Generating {0} members...", numberToGenerate));
            var members = new List<Member>(numberToGenerate);

            for (int i = 0; i < numberToGenerate; i++)
            {
                var member = GenerateMember();
                members.Add(member);
            }

            logAction(numberToGenerate + " members generated successfully");
            logAction("Saving members...");
            Members membersDb = new Members();
            ProgressReporter reporter = new ProgressReporter(numberToGenerate);
            reporter.ReportProgress(members, m => membersDb.Insert(m), percent => logAction(percent + "% complete"));
            logAction(numberToGenerate + " members saved successfully");
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
