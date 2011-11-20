namespace GiveCRM.DummyDataGenerator
{
    using System;
    using System.Windows;
    using System.Configuration;
    using System.Threading;

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

            generator.Update += this.ShowUpdateOnUiThread;
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
            int memberCountValue = ReadMemberCount();
            ThreadPool.QueueUserWorkItem(o => this.generator.GenerateMembers(memberCountValue));
        }

        private void LoadMembers(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o => this.generator.LoadMembers()); 
        }

        private void GenerateCampaign(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o => this.generator.GenerateCampaign()); 
        }

        private void GenerateCampaignAndDonations(object sender, RoutedEventArgs e)
        {
            int donationMinAmount = ReadDonationAmountMin();
            int donationMaxAmount = ReadDonationAmountMax();
            int donationCountMax = ReadDonationCountMax();

            ThreadPool.QueueUserWorkItem(o =>
                {
                    generator.GenerateCampaign();
                    generator.GenerateDonations(donationMinAmount, donationMaxAmount, donationCountMax);
                }); 
        }

        private void GenerateDonations(object sender, RoutedEventArgs e)
        {
            int donationMinAmount = ReadDonationAmountMin();
            int donationMaxAmount = ReadDonationAmountMax();
            int donationCountMax = ReadDonationCountMax();

            ThreadPool.QueueUserWorkItem(o => generator.GenerateDonations(donationMinAmount, donationMaxAmount, donationCountMax)); 
        }

        private int ReadMemberCount()
        {
            // default data size = 10 000 members
            return this.StringToIntWithDefault(memberCountText.Text, 10000);
        }

        private int ReadDonationAmountMin()
        {
            return StringToIntWithDefault(donationAmountMinText.Text, 5);
        }

        private int ReadDonationAmountMax()
        {
            return StringToIntWithDefault(donationAmountMaxText.Text, 100);
        }

        private int ReadDonationCountMax()
        {
            return StringToIntWithDefault(donationCountMaxText.Text, 1);
        }

        private int StringToIntWithDefault(string stringValue, int defaultValue)
        {
            if (string.IsNullOrEmpty(stringValue))
            {
                return defaultValue;
            }

            return int.Parse(stringValue);
        }

        private void ShowUpdateOnUiThread(object sender, EventArgs<string> e)
        {
            Action update = () => outputStatus.Text = e.Data;
            Dispatcher.Invoke(update);
        }
    }
}
