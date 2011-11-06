using System;
using System.Collections.Generic;
using System.IO;
using GiveCRM.ImportExport.Cells;
using Cell = GiveCRM.ImportExport.Cells.Cell;

namespace GiveCRM.ImportExport
{
    public class ExcelExport:IDisposable, IExcelExport
    {
        internal IExcelExporter ExcelExporter;
        private bool disposed;

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
            if (outputStream == null)
            {
                throw new ArgumentNullException("outputStream");
            }
            if (ExcelExporter == null)
            {
                throw new InvalidOperationException("WriteDataToExport must be called before ExportToStream");
            }
            ExcelExporter.ExportToStream(outputStream);
        }

        /// <summary>
        /// Exports the data supplied to an excel workbook
        /// </summary>
        /// <param name="rowData">The data to be exported to the workbook</param>
        /// <param name="formatter"></param>
        public void WriteDataToExport(IEnumerable<IEnumerable<Cell>> rowData, CellFormatter formatter, ExcelFileType excelFileType)
        {
            if (excelFileType == ExcelFileType.XLS)
            {
                ExcelExporter = new ExcelXlsExporter();
            }

            ExcelExporter.WriteDataToExport(rowData, formatter, "Sheet 1");
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
                    if (ExcelExporter != null)
                    {
                        ExcelExporter.Dispose(); 
                    }
                }

                disposed = true;
            }
        }
    }
}