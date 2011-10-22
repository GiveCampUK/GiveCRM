using System;
using System.Collections.Generic;
using System.IO;

namespace GiveCRM.ImportExport
{
    internal interface IExcelImporter : IDisposable
    {
        void Open(Stream stream);
        IEnumerable<IEnumerable<string>> GetRows(int sheetIndex, bool hasHeaderRow);
        IEnumerable<IDictionary<string, object>> GetRowsAsKeyValuePairs(int sheetIndex);
    }
}
