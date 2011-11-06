using System;
using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace GiveCRM.ImportExport
{
    public class ExcelXlsImporter : IExcelImporter
    {
        internal HSSFWorkbook Workbook;
        private bool disposed;

        public void Open(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            Workbook = new HSSFWorkbook(stream);
        }

        public IEnumerable<IEnumerable<string>> GetRows(int sheetIndex, bool hasHeaderRow)
        {
            var sheet = Workbook.GetSheetAt(sheetIndex);
            var rows = new List<List<string>>();

            var count = 0;
            if (hasHeaderRow)
            {
                count++;
            }
            for (var i = count; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                var cells = new List<string>();
                for (int j = 0; j < row.LastCellNum; j++)
                {
                    cells.Add(GetCellValue(row.GetCell(j)));
                }
                rows.Add(cells);
            }

            return rows;
        }

        public IEnumerable<IDictionary<string, object>> GetRowsAsKeyValuePairs(int sheetIndex)
        {
            var sheet = Workbook.GetSheetAt(sheetIndex);
            var headerRowValues = new List<string>();
            var rows = new List<IDictionary<string,object>>();

            var count = 0;
            var headerRow = sheet.GetRow(count);

            for (int j = 0; j < headerRow.LastCellNum; j++)
            {
                headerRowValues.Add(GetCellValue(headerRow.GetCell(j)));
            }
            count++;

            for (var i = count; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                var cells = new Dictionary<string, object>();
                for (int j = 0; j < row.LastCellNum; j++)
                {
                    cells.Add(headerRowValues[j], GetCellValue(row.GetCell(j)));
                }
                rows.Add(cells);
            }

            return rows;
        }

        private string GetCellValue(Cell cell)
        {
            if (cell == null)
            {
                return String.Empty;
            }
            
            switch (cell.CellType)
            {
                case CellType.NUMERIC:
                    return GetDateOrNumericCellValue(cell);

                default:
                    return cell.StringCellValue;
            }
        }

        private static string GetDateOrNumericCellValue(Cell cell)
        {
            if (DateUtil.IsCellDateFormatted(cell))
            {
                return cell.DateCellValue.ToShortDateString();
            }
            return cell.NumericCellValue.ToString();
        }

        ~ExcelXlsImporter()
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
                Workbook.Dispose();
                disposed = true;
            }
        }
    }
}
