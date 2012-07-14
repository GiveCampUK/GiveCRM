namespace GiveCRM.DummyDataGenerator.Data
{
    using System.Collections.Generic;

    internal static class EmailData
    {
        internal static readonly IList<string> Separators = new List<string> {string.Empty, ".", "_"};

        /// <summary>
        /// It's better if these aren't popular email providers to avoid
        /// potential accidents where a real email address is accidentally targeted.
        /// </summary>
        internal static readonly IList<string> Domains = new List<string>
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
