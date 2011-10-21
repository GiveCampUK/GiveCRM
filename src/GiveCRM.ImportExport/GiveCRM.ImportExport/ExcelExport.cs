using System;
using System.Collections.Generic;
using System.IO;
using GiveCRM.ImportExport.Cells;
using NPOI.SS.UserModel;
using Cell = GiveCRM.ImportExport.Cells.Cell;

namespace GiveCRM.ImportExport
{
    public class ExcelExport:IDisposable
    {
        private readonly Workbook workBook;
        private Sheet currentSheet;
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelExport"/> class.
        /// </summary>
        public ExcelExport()
        {
            workBook = new NPOI.HSSF.UserModel.HSSFWorkbook();
        }

        ~ExcelExport()
        {
            Dispose(false);
        }

        /// <summary>
        /// Exports the work book to the output stream already provided.
        /// </summary>
        /// <returns>Stream holding the Excel workbook</returns>
        public void ExportToStream(Stream outputStream)
        {
            workBook.Write(outputStream);
            outputStream.Flush();
            outputStream.Position = 0;
        }

        /// <summary>
        /// Exports the data supplied to an excel workbook
        /// </summary>
        /// <param name="rowData">The data to be exported to the workbook</param>
        /// <param name="formatter"></param>
        public void WriteDataToExport(IEnumerable<IEnumerable<Cell>> rowData, CellFormatter formatter)
        {
            if (currentSheet == null)
            {
                currentSheet = workBook.CreateSheet("Sheet 1");
            }

            formatter.WriteDataToSheet(currentSheet, rowData);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    currentSheet.Dispose();
                    workBook.Dispose();
                }

                disposed = true;
            }
        }
    }
}