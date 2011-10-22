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
            generationOutput.Text = generator.GenerateMembers();
        }

        private void GenerateCampaign(object sender, RoutedEventArgs e)
        {
            generationOutput.Text = generator.GenerateCampaign();
        }

        private void GenerateDonations(object sender, RoutedEventArgs e)
        {
            generationOutput.Text = generator.GenerateDonations();

        }
    }
}
