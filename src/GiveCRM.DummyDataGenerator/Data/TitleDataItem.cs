namespace GiveCRM.DummyDataGenerator.Data
{
    internal class TitleDataItem
    {
        public string Title{get;set;}
        public string Salutation{get;set;}

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
