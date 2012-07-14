﻿namespace GiveCRM.ImportExport
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using NPOI.SS.Formula;
    using OfficeOpenXml;

    public class ExcelXlsxImporter : IExcelImporter
    {
        internal ExcelWorkbook Workbook;
        internal ExcelPackage Package;
        private bool disposed;

        public void Open(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            Package = new ExcelPackage(stream);
            if (Package.Workbook == null)
            {
                throw new WorkbookNotFoundException("Workbook not found.");
            }
            Workbook = Package.Workbook;
        }

        public IEnumerable<IEnumerable<string>> GetRows(int sheetIndex, bool hasHeaderRow)
        {
            var rows = new List<List<string>>();

            if (Workbook.Worksheets.Count > 0)
            {
                var sheet = Workbook.Worksheets[sheetIndex];
                var count = 1;
                if (hasHeaderRow)
                {
                    count++;
                }
                for (int i = count; i <= sheet.Dimension.End.Row; i++)
                {
                    var cells = new List<string>();
                    for (int j = 1; j <= sheet.Dimension.End.Column; j++)
                    {
                        cells.Add(sheet.Cells[i, j].GetValue<string>());
                    }
                    rows.Add(cells);
                } 
            }

            return rows;
        }

        public IEnumerable<IDictionary<string, object>> GetRowsAsKeyValuePairs(int sheetIndex)
        {
            var headerRowValues = new List<string>();
            var rows = new List<IDictionary<string, object>>();

            if (Workbook.Worksheets.Count > 0)
            {
                var sheet = Workbook.Worksheets[sheetIndex + 1];

                var count = 1;
                for (int j = 1; j <= sheet.Dimension.End.Column; j++)
                {
                    headerRowValues.Add(sheet.Cells[count, j].GetValue<string>() ?? String.Empty);
                }
                count++;
                for (int i = count; i <= sheet.Dimension.End.Row; i++)
                {
                    var cells = new Dictionary<string, object>();
                    for (int j = 1; j <= sheet.Dimension.End.Column; j++)
                    {
                        cells.Add(headerRowValues[j - 1], sheet.Cells[i, j].GetValue<string>() ?? String.Empty);
                    }
                    rows.Add(cells);
                } 
            }

            return rows;
        }

        ~ExcelXlsxImporter()
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
                if (Package != null)
                {
                    Package.Dispose(); 
                }
                disposed = true;
            }
        }
    }
}
