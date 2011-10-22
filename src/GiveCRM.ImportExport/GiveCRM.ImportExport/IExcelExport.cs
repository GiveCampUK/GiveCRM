using System.Collections.Generic;
using System.IO;
using GiveCRM.ImportExport.Cells;

namespace GiveCRM.ImportExport
{
    public interface IExcelExport
    {
        /// <summary>
        /// Exports the work book to the output stream already provided.
        /// </summary>
        /// <returns>Stream holding the Excel workbook</returns>
        void ExportToStream(Stream outputStream);

        /// <summary>
        /// Exports the data supplied to an excel workbook
        /// </summary>
        /// <param name="rowData">The data to be exported to the workbook</param>
        /// <param name="formatter"></param>
        /// <param name="excelFileType"></param>
        void WriteDataToExport(IEnumerable<IEnumerable<Cell>> rowData, CellFormatter formatter, ExcelFileType excelFileType);
    }
}