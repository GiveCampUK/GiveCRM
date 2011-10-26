using System;
using System.Collections.Generic;

namespace GiveCRM.ImportExportService
{
    public class ImportDataCompletedEventArgs : EventArgs
    {
        public IEnumerable<IDictionary<string, object>> ImportedData { get; set; }
    }
}