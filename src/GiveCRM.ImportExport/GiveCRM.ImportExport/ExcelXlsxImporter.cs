using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;

namespace GiveCRM.ImportExport
{
    public class ExcelXlsxImporter : IExcelImporter
    {
        internal ExcelWorkbook Workbook;
        internal ExcelPackage Package;
        private bool _disposed;

        public void Open(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            Package = new ExcelPackage(stream);
            Workbook = Package.Workbook;
        }

        public IEnumerable<IEnumerable<string>> GetRows(int sheetIndex, bool hasHeaderRow)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDictionary<string, object>> GetRowsAsKeyValuePairs(int sheetIndex)
        {
            throw new NotImplementedException();
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
            if (!_disposed)
            {

                _disposed = true;
            }
        }
    }
}
