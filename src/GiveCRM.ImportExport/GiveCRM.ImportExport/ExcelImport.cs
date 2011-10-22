using System;
using System.Collections.Generic;
using System.IO;

namespace GiveCRM.ImportExport
{
    public class ExcelImport:IDisposable, IExcelImport
    {
        private bool _disposed;
        private ExcelFileType fileType;
        private readonly bool hasHeaderRow;

        internal IExcelImporter ExcelImporter;

        public ExcelImport(ExcelFileType fileType, bool hasHeaderRow)
        {
            this.fileType = fileType;
            this.hasHeaderRow = hasHeaderRow;
        }

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
                if (ExcelImporter != null)
                {
                    ExcelImporter.Dispose(); 
                }
                _disposed = true;
            }
        }

        public void Open(Stream streamToProcess)
        {
            if (streamToProcess == null) throw new ArgumentNullException("streamToProcess");

            if (fileType == ExcelFileType.XLS)
            {
                ExcelImporter = new ExcelXlsImporter();
            }
            else
            {
                ExcelImporter = new ExcelXlsxImporter();
            }

            ExcelImporter.Open(streamToProcess);
        }

        public IEnumerable<IEnumerable<string>> GetRows(int sheetIndex, bool includeHeaderRow)
        {
            if (!hasHeaderRow && includeHeaderRow)
            {
                throw new ArgumentException("Cannot include header row when hasHeaderRow is false");
            }

            return ExcelImporter.GetRows(sheetIndex, includeHeaderRow);
        }

        public IEnumerable<IDictionary<string, object>> GetRowsAsKeyValuePairs(int sheetIndex)
        {
            if (!hasHeaderRow)
            {
                throw new InvalidOperationException(
                    "Unable to return data as Key Value Pair when hasHeaderRow is false as relies on header row to provide key names");
            }

            return ExcelImporter.GetRowsAsKeyValuePairs(sheetIndex);
        }
    }
}