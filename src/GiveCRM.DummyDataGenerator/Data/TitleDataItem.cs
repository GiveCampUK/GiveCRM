namespace GiveCRM.DummyDataGenerator.Data
{
    internal class TitleDataItem
    {
        public string Title{get;private set;}
        public string Salutation{get;private set;}

        public int Frequency { get; private set; }

        public TitleDataItem(string title, string salutation, int frequency)
        {
            this.Title = title;
            this.Salutation = salutation;
            this.Frequency = frequency;
        }

        public override string ToString()
        {
            return Title + " " + Salutation;
        }
    }
}
