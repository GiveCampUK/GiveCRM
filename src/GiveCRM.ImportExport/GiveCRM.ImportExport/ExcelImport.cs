using System;
using System.Collections.Generic;
using System.IO;
using GiveCRM.ImportExport.Cells;
using NPOI.HSSF.UserModel;
using OfficeOpenXml;

namespace GiveCRM.ImportExport
{
    public class ExcelImport:IDisposable, IExcelImport
    {
        private bool _disposed;

        internal IExcelImporter ExcelImporter;

        ~ExcelImport()
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
            if (!_disposed)
            {
                ExcelImporter.Dispose();
                _disposed = true;
            }
        }

        public void OpenXlsx(Stream streamToProcess)
        {
            if (streamToProcess == null) throw new ArgumentNullException("streamToProcess");

            ExcelImporter = new ExcelXlsxImporter();
            ExcelImporter.Open(streamToProcess);
        }

        public void OpenXls(Stream streamToProcess)
        {
            if (streamToProcess == null) throw new ArgumentNullException("streamToProcess");

            ExcelImporter = new ExcelXlsImporter();
            ExcelImporter.Open(streamToProcess);
        }

        public IEnumerable<IEnumerable<string>> GetRows(int sheetIndex, bool hasHeaderRow)
        {
            return ExcelImporter.GetRows(sheetIndex, hasHeaderRow);
        }
    }
}