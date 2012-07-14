namespace GiveCRM.DummyDataGenerator
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using GiveCRM.DummyDataGenerator.Generation;
    using Simple.Data;

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
                                          }, uiContext);
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
                                          }, uiContext);
        }

        private void GenerateMembersButton_Click(object sender, RoutedEventArgs e)
        {
            int numberOfMembersToGenerate = Convert.ToInt32(NumberOfMembersTextBox.Text);
            var generator = new MemberGenerator(Log);
            RunGeneration(() => generator.Generate(numberOfMembersToGenerate));
        }
        
        private void GenerateCampaignsButton_Click(object sender, RoutedEventArgs e)
        {
            int numberOfCampaignsToGenerate = Convert.ToInt32(NumberOfCampaignsTextBox.Text);
            var generator = new CampaignGenerator(Log);
            RunGeneration(() => generator.Generate(numberOfCampaignsToGenerate));
        }

        private void GenerateAllButton_Click(object sender, RoutedEventArgs e)
        {
            var generator = new DatabaseGenerator(Log);
            RunGeneration(generator.Generate);
        }

        private void RunGeneration(Action generationCallback)
        {
            // runs a generation task, disabling the generate buttons to prevent a new generate 
            //  task being started while one is still in progress
            TaskScheduler uiContext = TaskScheduler.FromCurrentSynchronizationContext();
            var task = Task.Factory.StartNew(() => SetGenerateButtonsState(false), CancellationToken.None, TaskCreationOptions.None, uiContext)
                                   .ContinueWith(_ => generationCallback(), TaskContinuationOptions.LongRunning);
            task.ContinueWith(_ =>
                                  {
                                      RefreshStats(uiContext);
                                      SetGenerateButtonsState(true);
                                  }, CancellationToken.None, TaskContinuationOptions.LongRunning, uiContext);
            task.ContinueWith(t =>
                                  {
                                      LogTaskExceptions(t);
                                      SetGenerateButtonsState(true);
                                  }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, uiContext);
        }

        private void SetGenerateButtonsState(bool state)
        {
            GenerateDatabaseButton.IsEnabled = state;
            GenerateMembersButton.IsEnabled = state;
            GenerateCampaignsButton.IsEnabled = state;
        }

        private void LogTaskExceptions(Task t)
        {
            string errorMessage = t.Exception == null
                                    ? "(No exception found)"
                                    : string.Join(Environment.NewLine, t.Exception.InnerExceptions.Select(ex => ex.Message));
            Log("Error: " + errorMessage);
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
