using System.Collections.Generic;

namespace GiveCRM.DummyDataGenerator.Data
{
    internal class TitleData
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
/*
        private readonly IList<TitleDataItem> maleTitlesData = new List<TitleDataItem>
            {
                new TitleDataItem("Mr.", "Mr.", 20),
                new TitleDataItem("Dr.", "Doctor", 1)
            };

        private readonly IList<TitleDataItem> femaleTitlesData = new List<TitleDataItem>
            {
                new TitleDataItem("Ms.", "Ms.", 10),
                new TitleDataItem("Mrs.", "Mrs.", 10),
                new TitleDataItem("Miss.", "Miss.", 10),
                new TitleDataItem("Dr.", "Doctor", 3)
            };

        internal readonly IList<TitleDataItem> MaleTitles = new List<TitleDataItem>();
        internal readonly IList<TitleDataItem> FemaleTitles = new List<TitleDataItem>();

        internal TitleData()
        {
            TitlesMultiplyIntoList(this.maleTitlesData, MaleTitles);
            TitlesMultiplyIntoList(this.femaleTitlesData, FemaleTitles);
        }

        private static void TitlesMultiplyIntoList(IEnumerable<TitleDataItem> source, IList<TitleDataItem> dest)
        {
            foreach (TitleDataItem titleDataItem in source)
            {
                for (int titleCount = 0; titleCount < titleDataItem.Frequency; titleCount++)
                {
                    dest.Add(titleDataItem);
                }
            }
        }
 */
    }
}
