using System;
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
    }
}