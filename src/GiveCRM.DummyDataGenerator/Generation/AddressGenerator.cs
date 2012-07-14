namespace GiveCRM.DummyDataGenerator.Generation
{
    using System.Collections.Generic;
    using GiveCRM.DummyDataGenerator.Data;

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
            string street = string.Format("{0} {1} {2}", random.PickFromList(StreetData.StreetNamePrefix), 
                                            random.PickFromList(StreetData.StreetNames), 
                                            random.PickFromList(StreetData.StreetSuffix)).Trim();
            string streetNumber = (random.NextInt(200) + 1).ToString();

            if (random.Percent(20))
            {
                streetNumber += random.Bool() ? "A" : "B";
            }

            return string.Format("{0} {1}", streetNumber, street);
        }

        internal string RandomPostalCode()
        {
            TownDataItem townData = random.PickFromList(TownData.Data);
            string postCodePrefix = random.PickFromList(townData.PostalCodePrefixes);
            return RandomPostalCode(postCodePrefix);
        }

        private string RandomPostalCode(string prefix)
        {
            string firstHalf = prefix + random.NextInt(10);
            string secondHalf = random.NextInt(10) + random.Letter() + random.Letter();
            return string.Format("{0} {1}", firstHalf, secondHalf);
        }
    }
}
