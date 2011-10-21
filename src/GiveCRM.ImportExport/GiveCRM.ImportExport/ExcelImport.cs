using System;
using System.IO;
using NPOI;

namespace GiveCRM.ImportExport
{
    public class ExcelImport:IDisposable
    {
        private bool disposed = false;

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
                disposed = true;
            }
        }

        public void Open(Stream streamToProcess)
        {
            if (streamToProcess == null) throw new ArgumentNullException("streamToProcess");
        }
    }
}