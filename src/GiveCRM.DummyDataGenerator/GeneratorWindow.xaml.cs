using System;
using System.Diagnostics;
using System.Linq;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using GiveCRM.DummyDataGenerator.Generation;
using Simple.Data;

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
                                          DebugPause();
                                          db = Database.OpenConnection(connectionString);
                                          Log("Connected to database successfully");
                                          DebugPause();
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
                        .ContinueWith(t =>
                                          {
                                              DebugPause();
                                              return new DatabaseStatisticsLoader().Load(db);
                                          }, TaskContinuationOptions.LongRunning)
                        .ContinueWith(t =>
                                          {
                                              DatabaseStatistics dbStats = t.Result;
                                              Log("Database statistics refreshed successfully");
                                              NumberOfMembersLabel.Content = dbStats.NumberOfMembers.ToString();
                                              NumberOfCampaignsLabel.Content = dbStats.NumberOfCampaigns.ToString();
                                              NumberOfDonationsLabel.Content = dbStats.NumberOfDonations.ToString();
                                          }, CancellationToken.None, TaskContinuationOptions.None, uiContext);
        }

        private void GenerateMembersButton_Click(object sender, RoutedEventArgs e)
        {
            int numberOfMembersToGenerate = Convert.ToInt32(NumberOfMembersTextBox.Text);
            string connectionString = DatbaseConnectionStringTextBox.Text;
            var generator = new MemberGenerator(connectionString, Log);

            TaskScheduler uiContext = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(() => generator.GenerateMembers(numberOfMembersToGenerate), TaskCreationOptions.LongRunning)
                        .ContinueWith(_ => RefreshStats(uiContext))
                        .ContinueWith(LogTaskExceptions, TaskContinuationOptions.OnlyOnFaulted);
        }
        
        private void GenerateAllButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = DatbaseConnectionStringTextBox.Text;
            var generator = new DatabaseGenerator(connectionString, Log);

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
                                       logArea.Text += text + Environment.NewLine;
                                       logArea.ScrollToEnd();
                                   };
            Dispatcher.Invoke(logAction);
        }

        [Conditional("DEBUG")]
        private void DebugPause()
        {
            Log("Pause...");
            Thread.Sleep(3000);
        }
    }
}
