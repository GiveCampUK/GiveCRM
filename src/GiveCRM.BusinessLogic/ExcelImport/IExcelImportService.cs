namespace GiveCRM.BusinessLogic.ExcelImport
{
    using System;
    using System.IO;
    using GiveCRM.ImportExport;

    public interface IExcelImportService
    {
        event Action<object, ImportDataCompletedEventArgs> ImportCompleted;
        event Action<object, ImportDataFailedEventArgs> ImportFailed;
        void Import(ExcelFileType fileType, Stream fileStream);
    }
}