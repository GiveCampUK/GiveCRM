using System;
using System.Collections.Generic;

namespace GiveCRM.Admin.BusinessLogic
{
    public class ImportDataCompletedEventArgs : EventArgs
    {
        public IEnumerable<IDictionary<string, object>> ImportedData { get; set; }
    }
}