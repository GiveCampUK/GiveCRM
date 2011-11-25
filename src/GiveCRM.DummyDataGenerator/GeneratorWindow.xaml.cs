using System;
using System.Linq;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Simple.Data;
using GiveCRM.DummyDataGenerator.Generation;

namespace GiveCRM.DummyDataGenerator
{
    public partial class GeneratorWindow
    {
        private volatile dynamic db;

        public GeneratorWindow()
        {
            InitializeComponent();

            var connectionStringSetting = ConfigurationManager.ConnectionStrings["GiveCRM"];
            DatbaseConnectionStringTextBox.Text = connectionStringSetting.ConnectionString;
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = DatbaseConnectionStringTextBox.Text;
            TaskScheduler uiContext = TaskScheduler.FromCurrentSynchronizationContext();

            Task.Factory.StartNew(() =>
                                      {
                                          Log("Connecting to database...");
                                          db = Database.OpenConnection(connectionString);
                                          Log("Connected to database successfully");
                                      }, TaskCreationOptions.LongRunning)
                        .ContinueWith(_ => RefreshStats(uiContext))
                        .ContinueWith(_ =>
                                          {
                                              ConnectionDockPanel.IsEnabled = false;
                                              TabControl.IsEnabled = true;
                                          }, CancellationToken.None, TaskContinuationOptions.None, uiContext);
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            TaskScheduler uiContext = TaskScheduler.FromCurrentSynchronizationContext();
            RefreshStats(uiContext);
        }

        private void RefreshStats(TaskScheduler uiContext)
        {
            Task.Factory.StartNew(() => Log("Refreshing database statistics..."), CancellationToken.None, TaskCreationOptions.None, uiContext)
                        .ContinueWith(t => new DatabaseStatisticsLoader().Load(db), TaskContinuationOptions.LongRunning)
                        .ContinueWith(t =>
                                          {
                                              DatabaseStatistics dbStats = t.Result;
                                              Log("Database statistics refreshed successfully");
                                              NumberOfMembersLabel.Content = dbStats.NumberOfMembers.ToString();
                                              NumberOfCampaignsLabel.Content = dbStats.NumberOfCampaigns.ToString();
                                              NumberOfSearchFiltersLabel.Content = dbStats.NumberOfSearchFilters.ToString();
                                              NumberOfDonationsLabel.Content = dbStats.NumberOfDonations.ToString();
                                          }, CancellationToken.None, TaskContinuationOptions.None, uiContext);
        }

        private void GenerateMembersButton_Click(object sender, RoutedEventArgs e)
        {
            int numberOfMembersToGenerate = Convert.ToInt32(NumberOfMembersTextBox.Text);
            var generator = new MemberGenerator(Log);

            TaskScheduler uiContext = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(() => generator.Generate(numberOfMembersToGenerate), TaskCreationOptions.LongRunning)
                        .ContinueWith(_ => RefreshStats(uiContext))
                        .ContinueWith(LogTaskExceptions, TaskContinuationOptions.OnlyOnFaulted);
        }
        
        private void GenerateAllButton_Click(object sender, RoutedEventArgs e)
        {
            var generator = new DatabaseGenerator(Log);

            TaskScheduler uiContext = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(generator.Generate)
                        .ContinueWith(_ => RefreshStats(uiContext))
                        .ContinueWith(LogTaskExceptions, TaskContinuationOptions.OnlyOnFaulted);
        }

        private void LogTaskExceptions(Task t)
        {
            string errorMessage = t.Exception == null
                                    ? "(No exception found)"
                                    : string.Join(Environment.NewLine, t.Exception.InnerExceptions.Select(ex => ex.Message));
            Log(errorMessage);
        }

        private void Log(string text)
        {
            Action logAction = () =>
                                   {
                                       logArea.Text += Environment.NewLine + text;
                                       logArea.ScrollToEnd();
                                   };
            Dispatcher.Invoke(logAction);
        }
    }
}
