using System;
using System.IO;

namespace GiveCRM.Web.Services.ExcelImport
{
    public interface IExcelImportService
    {
        event Action<object, ImportDataCompletedEventArgs> ImportCompleted;
        event Action<object, ImportDataFailedEventArgs> ImportFailed;
        void Import(Stream file);
    }
}