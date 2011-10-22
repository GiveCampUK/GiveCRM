namespace GiveCRM.DummyDataGenerator
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private readonly Generator generator = new Generator();

        public Main()
        {
            InitializeComponent();
        }

        private void GenerateMembers(object sender, RoutedEventArgs e)
        {
            // default data size = 100 000 members
            const string DefaultCountToGenerate = "10000";
            if (string.IsNullOrEmpty(memberCount.Text))
            {
                memberCount.Text = DefaultCountToGenerate;
            }

            int memberCountValue = int.Parse(memberCount.Text);
            generationOutput.Text = generator.GenerateMembers(memberCountValue);
        }

        private void LoadMembers(object sender, RoutedEventArgs e)
        {
            generationOutput.Text = generator.LoadMembers();
        }

        private void GenerateCampaign(object sender, RoutedEventArgs e)
        {
            generationOutput.Text = generator.GenerateCampaign();
        }

        private void GenerateCampaignAndDonations(object sender, RoutedEventArgs e)
        {
            generationOutput.Text = generator.GenerateCampaign();
            generationOutput.Text = generator.GenerateDonations();
        }

        private void GenerateDonations(object sender, RoutedEventArgs e)
        {
            generationOutput.Text = generator.GenerateDonations();
        }
    }
}
