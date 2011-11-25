using System;

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

        protected void GenerateMultiple(int numberToGenerate, Action createItemCallback)
        {
            ProgressReporter reporter = new ProgressReporter(numberToGenerate);
            LogAction(string.Format("Generating {0} {1}...", numberToGenerate, GeneratedItemType));

            reporter.ReportProgress(createItemCallback, percent => LogAction(percent + "% complete"));
            LogAction(string.Format("{0} {1} generated successfully", numberToGenerate, GeneratedItemType));
        }
    }
}