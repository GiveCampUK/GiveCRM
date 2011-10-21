using System.Collections.Generic;
using ExcelExport.Cells;
using NPOI.SS.UserModel;

namespace ExcelExport.Formatters
{
    public interface IFormatter
    {
        void WriteDataToSheet(Sheet sheet, IEnumerable<IEnumerable<Cell>> rowData);
    }
}