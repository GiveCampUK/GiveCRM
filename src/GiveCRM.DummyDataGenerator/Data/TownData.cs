using System.Collections.Generic;

namespace GiveCRM.DummyDataGenerator.Data
{
    public class TownData
    {
        public string Town { get; set; }

        public IList<string> PostalCodePrefixes { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
    }
}
