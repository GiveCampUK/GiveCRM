using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GiveCRM.ImportExport;
using Simple.Data;

namespace GiveCRM.Admin.BusinessLogic
{
    public class ExcelImportService : IExcelImportService
    {
        private readonly dynamic db = Database.OpenNamedConnection("GiveCRM");

        //CREATE TABLE [dbo].[Member]
        //(
        //    ID int identity(1,1) NOT NULL PRIMARY KEY, 
        //    Reference nvarchar(20) NOT NULL,
        //    Title nvarchar(20) NULL,
        //    FirstName nvarchar(50) NOT NULL,
        //    LastName nvarchar(50) NOT NULL,
        //    Salutation nvarchar(50) NOT NULL,
        //    EmailAddress nvarchar(50) NULL,
        //    AddressLine1 nvarchar(50) NULL,
        //    AddressLine2 nvarchar(50) NULL,
        //    Town nvarchar(50) NULL,
        //    Region nvarchar(50) NULL,
        //    PostalCode nvarchar(50) NULL,
        //    Country nvarchar(50) NULL,
        //    Archived bit NOT NULL
        //)

        private readonly IExcelImport importer;

        public ExcelImportService(IExcelImport importer)
        {
            if (importer == null)
            {
                throw new ArgumentNullException("importer");
            }

            this.importer = importer;
        }

        #region IExcelImportService

        public event Action<object, ImportDataCompletedEventArgs> ImportCompleted;
        public event Action<object, ImportDataFailedEventArgs> ImportFailed;

        public void Import(Stream file)
        {
            //  FIELDS THAT CANNOT BE NULL!
            //    Reference
            //    FirstName
            //    LastName
            //    Salutation
            //    [Archived] - Not in template.

            try
            {
                // Hard-coded for now - FIX THIS!!!
                const ExcelFileType fileType = ExcelFileType.XLS;
                importer.Open(file, fileType, hasHeaderRow: true);

                const int sheetIndex = 0; // Hard-coded for now
                IEnumerable<IDictionary<string, object>> rowsAsKeyValuePairs =
                    importer.GetRowsAsKeyValuePairs(sheetIndex);

                AddArchivedFieldToData(rowsAsKeyValuePairs); // This is a non-null field
                db.Members.Insert(rowsAsKeyValuePairs);

                InvokeImportDataCompleted();
            } 
            catch(Exception ex)
            {
                InvokeImportErrorFailed(ex);
            }
        }

        #endregion

        private static void AddArchivedFieldToData(IEnumerable<IDictionary<string, object>> rowsAsKeyValuePairs)
        {
            foreach (var row in rowsAsKeyValuePairs)
            {
                object archived;
                if (!row.TryGetValue("Archived", out archived))
                {
                    row["Archived"] = false;
                }
            }
        }

        #region Invoke Events

        private void InvokeImportDataCompleted()
        {
            var handler = ImportCompleted;
            if (handler != null)
            {
                handler(this, new ImportDataCompletedEventArgs());
            }
        }

        private void InvokeImportErrorFailed(Exception exception)
        {
            if (exception == null)
            {
                // No exception, no failure
                return;
            }

            var handler = ImportFailed;
            if (handler != null)
            {
                handler(this, new ImportDataFailedEventArgs(exception));
            }
        }

        #endregion

    }
}
