namespace GiveCRM.DummyDataGenerator.Data
{
    internal class TitleData
    {
        public string Title { get; set; }

        public string Salutation { get; set; }

        public TitleData(string title, string salutation)
        {
            this.Title = title;
            this.Salutation = salutation;
        }
    }
}
