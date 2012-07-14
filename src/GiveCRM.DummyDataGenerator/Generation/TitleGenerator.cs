namespace GiveCRM.DummyDataGenerator.Generation
{
    using System.Collections.Generic;
    using GiveCRM.DummyDataGenerator.Data;

    internal class TitleGenerator
    {
        private readonly RandomSource random = new RandomSource();

        private readonly IList<TitleDataItem> femaleTitles;
        private readonly IList<TitleDataItem> maleTitles;

        public TitleGenerator()
        {
            femaleTitles = TitlesMultiplyIntoList(TitleData.FemaleTitles);
            maleTitles = TitlesMultiplyIntoList(TitleData.MaleTitles);
        }

        private static IList<TitleDataItem> TitlesMultiplyIntoList(IEnumerable<TitleDataItem> sourceData)
        {
            List<TitleDataItem> titlesList = new List<TitleDataItem>();

            foreach (TitleDataItem titleDataItem in sourceData)
            {
                for (int titleCount = 0; titleCount < titleDataItem.Frequency; titleCount++)
                {
                    titlesList.Add(titleDataItem);
                }
            }

            return titlesList;
        }

        internal TitleDataItem GenerateFemaleTitle()
        {
            return random.PickFromList(femaleTitles);
        }

        internal TitleDataItem GenerateMaleTitle()
        {
            return random.PickFromList(maleTitles);
        }
    }
}
