using System;

namespace GiveCRM.Admin.BusinessLogic
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