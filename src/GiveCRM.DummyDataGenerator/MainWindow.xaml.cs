using System.Configuration;

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
            ShowDataseConnection();
        }

        private void ShowDataseConnection()
        {
            var connection = ConfigurationManager.ConnectionStrings["GiveCrm"];
            if (connection == null)
            {
                databaseConnectionText.Text = "Database connection for GiveCRM was not found";
            }
            else
            {
                databaseConnectionText.Text = "Database is GiveCRM: " + connection.ConnectionString;                
            }
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
            outputStatus.Text = generator.GenerateMembers(memberCountValue);
        }

        private void LoadMembers(object sender, RoutedEventArgs e)
        {
            outputStatus.Text = generator.LoadMembers();
        }

        private void GenerateCampaign(object sender, RoutedEventArgs e)
        {
            outputStatus.Text = generator.GenerateCampaign();
        }

        private void GenerateCampaignAndDonations(object sender, RoutedEventArgs e)
        {
            outputStatus.Text = generator.GenerateCampaign();
            outputStatus.Text = generator.GenerateDonations();
        }

        private void GenerateDonations(object sender, RoutedEventArgs e)
        {
            outputStatus.Text = generator.GenerateDonations();
        }
    }
}
