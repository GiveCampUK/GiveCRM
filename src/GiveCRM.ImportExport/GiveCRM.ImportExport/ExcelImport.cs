namespace GiveCRM.ImportExport
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class ExcelImport:IDisposable, IExcelImport
    {
        private bool disposed;
        private bool hasHeaderRow;

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
            if (!disposed)
            {
                if (ExcelImporter != null)
                {
                    ExcelImporter.Dispose(); 
                }
                disposed = true;
            }
        }

        public void Open(Stream streamToProcess, ExcelFileType fileType, bool hasHeaderRow)
        {
            if (streamToProcess == null)
            {
                throw new ArgumentNullException("streamToProcess");
            }

            this.hasHeaderRow = hasHeaderRow;

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