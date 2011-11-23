namespace GiveCRM.DummyDataGenerator.Data
{
    internal class TitleDataItem
    {
        public string Title{get;private set;}
        public string Salutation{get;private set;}

        public TitleDataItem(string title, string salutation)
        {
            this.Title = title;
            this.Salutation = salutation;
        }

        public override string ToString()
        {
            return Title + " " + Salutation;
        }
    }
}
