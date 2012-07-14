namespace GiveCRM.ImportExport
{
    using System.Collections.Generic;
    using System.IO;

    public interface IExcelImport
    {
        void Open(Stream streamToProcess, ExcelFileType fileType, bool hasHeaderRow);
        IEnumerable<IEnumerable<string>> GetRows(int sheetIndex, bool includeHeaderRow);
        IEnumerable<IDictionary<string, object>> GetRowsAsKeyValuePairs(int sheetIndex);
    }
}