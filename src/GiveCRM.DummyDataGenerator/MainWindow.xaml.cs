using System;
using System.Configuration;
using System.Threading;

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
            ThreadPool.QueueUserWorkItem(o =>
                {
                    generator.GenerateCampaign();
                    generator.GenerateDonations();
                }); 
        }

        private void GenerateDonations(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o => generator.GenerateDonations()); 
        }

        private int ReadMemberCount()
        {
            // default data size = 100 000 members
            const string DefaultCountToGenerate = "10000";
            if (string.IsNullOrEmpty(this.memberCountText.Text))
            {
                this.memberCountText.Text = DefaultCountToGenerate;
            }

            return int.Parse(this.memberCountText.Text);
        }

        private void ShowUpdateOnUiThread(object sender, EventArgs<string> e)
        {
            Action update = () => outputStatus.Text = e.Data;
            Dispatcher.Invoke(update);
        }
    }
}
