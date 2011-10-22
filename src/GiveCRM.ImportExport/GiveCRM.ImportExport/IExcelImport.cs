using System.Collections.Generic;
using System.IO;

namespace GiveCRM.ImportExport
{
    public interface IExcelImport
    {
        void OpenXlsx(Stream streamToProcess);
        void OpenXls(Stream streamToProcess);
        IEnumerable<IEnumerable<string>> GetRows(int sheetIndex, bool hasHeaderRow);
    }
}