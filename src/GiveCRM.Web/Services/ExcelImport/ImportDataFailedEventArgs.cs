using System;

namespace GiveCRM.Web.Services.ExcelImport
{
    public class ImportDataFailedEventArgs : EventArgs
    {
        public ImportDataFailedEventArgs(Exception exception)
        {
            Exception = exception;
        }

        public Exception Exception { get; private set; }
    }
}