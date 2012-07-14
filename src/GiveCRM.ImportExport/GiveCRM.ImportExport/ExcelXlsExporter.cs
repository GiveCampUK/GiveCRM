namespace GiveCRM.ImportExport
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using GiveCRM.ImportExport.Cells;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using Cell = GiveCRM.ImportExport.Cells.Cell;

    public class ExcelXlsExporter:IExcelExporter
    {
        private readonly Workbook workBook;
        private Sheet currentSheet;
        private bool disposed = false;

        public ExcelXlsExporter()
        {
            workBook = new HSSFWorkbook();
        }

        public void ExportToStream(Stream outputStream)
        {
            workBook.Write(outputStream);
            outputStream.Flush();
            outputStream.Position = 0;
        }

        public void WriteDataToExport(IEnumerable<IEnumerable<Cell>> rowData, CellFormatter formatter, string sheetName)
        {
            if (currentSheet == null)
            {
                currentSheet = workBook.CreateSheet(sheetName);
            }

            formatter.WriteDataToSheet(currentSheet, rowData);
        }

        ~ExcelXlsExporter()
        {
            Dispose(false);
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
                if (workBook != null)
                {
                    currentSheet.Dispose();
                    workBook.Dispose(); 
                }
                disposed = true;
            }
        }
    }
}
