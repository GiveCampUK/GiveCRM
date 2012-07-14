namespace GiveCRM.BusinessLogic.ExcelImport
{
    using System;

    public class ImportDataFailedEventArgs : EventArgs
    {
        public ImportDataFailedEventArgs(Exception exception)
        {
            Exception = exception;
        }

        public Exception Exception { get; private set; }
    }
}