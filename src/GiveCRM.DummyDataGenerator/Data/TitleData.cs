using System.Collections.Generic;

namespace GiveCRM.DummyDataGenerator.Data
{
    internal static class TitleData
    {
        internal static readonly IList<TitleDataItem> MaleTitles = new List<TitleDataItem>
            {
                new TitleDataItem("Mr.", "Mr."),
                new TitleDataItem("Mr.", "Mr."),
                new TitleDataItem("Mr.", "Mr."),
                new TitleDataItem("Mr.", "Mr."),
                new TitleDataItem("Mr.", "Mr."),
                new TitleDataItem("Mr.", "Mr."),
                new TitleDataItem("Mr.", "Mr."),
                new TitleDataItem("Mr.", "Mr."),
                new TitleDataItem("Mr.", "Mr."),
                new TitleDataItem("Mr.", "Mr."),
                new TitleDataItem("Mr.", "Mr."),
                new TitleDataItem("Mr.", "Mr."),
                new TitleDataItem("Mr.", "Mr."),
                new TitleDataItem("Dr.", "Doctor")
            };

        internal static readonly IList<TitleDataItem> FemaleTitles = new List<TitleDataItem>
            {
                new TitleDataItem("Ms.", "Ms."),
                new TitleDataItem("Ms.", "Ms."),
                new TitleDataItem("Ms.", "Ms."),
                new TitleDataItem("Ms.", "Ms."),
                new TitleDataItem("Ms.", "Ms."),
                new TitleDataItem("Ms.", "Ms."),
                new TitleDataItem("Ms.", "Ms."),
                new TitleDataItem("Ms.", "Ms."),
                new TitleDataItem("Ms.", "Ms."),
                new TitleDataItem("Mrs.", "Mrs."),
                new TitleDataItem("Mrs.", "Mrs."),
                new TitleDataItem("Mrs.", "Mrs."),
                new TitleDataItem("Mrs.", "Mrs."),
                new TitleDataItem("Dr.", "Doctor")
            };
    }
}
