namespace GiveCRM.DummyDataGenerator.Data
{
    using System.Collections.Generic;

    internal class TitleData
    {
        internal static readonly IList<TitleDataItem> MaleTitles = new List<TitleDataItem>
                                                             {
                                                                         new TitleDataItem("Mr.", "Mr.", 20),
                                                                         new TitleDataItem("Dr.", "Doctor", 1)
                                                             };

        internal static readonly IList<TitleDataItem> FemaleTitles = new List<TitleDataItem>
                                                               {
                                                                           new TitleDataItem("Ms.", "Ms.", 10),
                                                                           new TitleDataItem("Mrs.", "Mrs.", 10),
                                                                           new TitleDataItem("Miss.", "Miss.", 10),
                                                                           new TitleDataItem("Dr.", "Doctor", 3)
                                                               };
    }
}
