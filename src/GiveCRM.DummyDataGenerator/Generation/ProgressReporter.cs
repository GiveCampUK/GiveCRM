using System;

namespace GiveCRM.DummyDataGenerator.Generation
{
    internal class ProgressReporter
    {
        private readonly int totalItems;
        private readonly int percentInterval;

        public ProgressReporter(int totalItems)
        {
            this.totalItems = totalItems;
            percentInterval = totalItems <= 1000 ? 10 : 1;
        }

        public void ReportProgress(Action actionToPerform, Action<int> percentCompleteCallback)
        {
            int lastPercentComplete = 0;

            for (int i = 0; i < totalItems; i++)
            {
                actionToPerform();
                int percentComplete = (int) Math.Floor(((i * 1.0) / totalItems) * 100);

                if (percentComplete - lastPercentComplete >= percentInterval)
                {
                    percentCompleteCallback(percentComplete);
                    lastPercentComplete = percentComplete;
                }
            }
        }
    }
}