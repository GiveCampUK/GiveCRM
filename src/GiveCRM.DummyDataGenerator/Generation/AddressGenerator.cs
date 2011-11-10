using System.Collections.Generic;
using GiveCRM.DummyDataGenerator.Data;

namespace GiveCRM.DummyDataGenerator.Generation
{
    internal class AddressGenerator
    {
        private readonly RandomSource random = new RandomSource();

        // this is used to check for and avoid generating duplicate values
        // use dictionary not list since contains checking is faster
        private readonly Dictionary<string, bool> generatedPostalAddresses = new Dictionary<string, bool>();

        internal AddressData GenerateStreetAddress()
        {
            string streetAddress;

            do
            {
                streetAddress = MakeStreetAddress();
            }
            while (generatedPostalAddresses.ContainsKey(streetAddress));

            generatedPostalAddresses.Add(streetAddress, true);
            TownDataItem townData = random.PickFromList(TownData.Data);
            string postCodePrefix = random.PickFromList(townData.PostalCodePrefixes);

            return new AddressData
                       {
                                   Address = streetAddress,
                                   PostalCode = RandomPostalCode(postCodePrefix),
                                   City = townData.Town,
                                   Region = townData.Region,
                                   Country = townData.Country
                       };
        }

        private string MakeStreetAddress()
        {
            string street = random.PickFromList(StreetData.StreetNamePrefix) + " " 
                + random.PickFromList(StreetData.StreetNames) + " " + random.PickFromList(StreetData.StreetSuffix);
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
    }
}