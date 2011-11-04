using System;
using System.Collections.Generic;

namespace GiveCRM.BusinessLogic.ExcelImport
{
    public class ImportDataCompletedEventArgs : EventArgs
    {
        public IEnumerable<IDictionary<string, object>> ImportedData { get; set; }
    }
}