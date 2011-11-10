using System;
using System.Linq;
using System.Collections.Generic;

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

        public void ReportProgress<T>(IEnumerable<T> items, Action<T> actionToPerform, Action<int> percentCompleteCallback)
        {
            int lastPercentComplete = 0;

            foreach (var iteration in items.Select((item, index) => new {Item = item, Index = index}))
            {
                actionToPerform(iteration.Item);
                int percentComplete = (int) Math.Floor(((iteration.Index * 1.0) / totalItems) * 100);

                if (percentComplete - lastPercentComplete >= percentInterval)
                {
                    percentCompleteCallback(percentComplete);
                    lastPercentComplete = percentComplete;
                }
            }
        }
    }
}