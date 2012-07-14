namespace GiveCRM.BusinessLogic.ExcelImport
{
    using System;
    using System.Collections.Generic;

    public class ImportDataCompletedEventArgs : EventArgs
    {
        public IEnumerable<IDictionary<string, object>> ImportedData { get; set; }
    }
}