namespace GiveCRM.Models.Search
{
    using System.Collections.Generic;

    public class LocationSearchCriteria : SearchCriteria
    {
        public const string City = "city";
        public const string Region = "region";
        public const string PostalCode = "postalcode";

        private static readonly HashSet<string> Names = new HashSet<string>
                                                            {
                                                                City,
                                                                Region,
                                                                PostalCode
                                                            };

        public static bool IsLocationSearchCriteria(string internalName)
        {
            return Names.Contains(internalName);
        }
    }
}