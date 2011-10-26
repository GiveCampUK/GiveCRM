using System.Collections.Generic;

namespace GiveCRM.DummyDataGenerator.Data
{
    internal static class EmailData
    {
        internal static List<string> Separators = new List<string> { "", ".", "_" };

        internal static List<string> Domains = new List<string>
            {
                "fakemail.com",
                "fakemail.net",
                "not.com",
                "not.net",
                "notmail.com",
                "notmail.net",
                "coldmail.net",
                "voidmail.net",
                "voidmail.org",
                "pmail.com",
                "pmail.net",
                "gahoo.com",
                "gahoo.org",
                "offthe.net",
                "notonthe.net",
            };
    }
}
