namespace GiveCRM.DummyDataGenerator.Data
{
    using System.Collections.Generic;

    internal static class CampaignNames
    {
        internal static readonly IList<string> CharitySuffix = new List<string>
            {
                "Trust",
                "Foundation",
                "Fund",
            };

        internal static readonly IList<string> CampaignSuffix = new List<string>
            {
                "Appeal",
                "Drive",
                "Request",
                "Campaign"
            };
    }
}
