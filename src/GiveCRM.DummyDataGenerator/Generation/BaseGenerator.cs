using System;
using System.Collections.Generic;

namespace GiveCRM.DummyDataGenerator.Generation
{
    internal abstract class BaseGenerator
    {
        internal abstract string GeneratedItemType{get;}
        internal abstract void Generate(int numberToGenerate);

        protected readonly Action<string> LogAction;

        protected BaseGenerator(Action<string> logAction)
        {
            this.LogAction = logAction;
        }

        protected void GenerateMultiple<T>(int numberToGenerate, Func<T> createItemCallback, Action<T> persistCallback)
        {
            LogAction(string.Format("Generating {0} {1}...", numberToGenerate, GeneratedItemType));
            var items = new List<T>(numberToGenerate);

            for (int i = 0; i < numberToGenerate; i++)
            {
                var item = createItemCallback();
                items.Add(item);
            }

            LogAction(string.Format("{0} {1} generated successfully", numberToGenerate, GeneratedItemType));
            LogAction(string.Format("Saving {0}...", GeneratedItemType));

            ProgressReporter reporter = new ProgressReporter(numberToGenerate);
            reporter.ReportProgress(items, persistCallback, percent => LogAction(percent + "% complete"));
            LogAction(string.Format(" {0} {1} saved successfully", numberToGenerate, GeneratedItemType));
        }
    }
}