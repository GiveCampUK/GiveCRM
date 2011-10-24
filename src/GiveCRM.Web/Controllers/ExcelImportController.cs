using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using GiveCRM.Admin.BusinessLogic;
using GiveCRM.Admin.Web.ViewModels;

namespace GiveCRM.Admin.Web.Controllers
{
    public class ExcelImportController : AsyncController
    {
        private const string ExcelFileExtension_OldFormat = ".xls";
        private const string ExcelFileExtension_NewFormat = ".xlsx";

        private readonly IExcelImportService excelImporter;
        
        public ExcelImportController(IExcelImportService excelImporter)
        {
            if (excelImporter == null)
            {
                throw new ArgumentNullException("excelImporter");
            }

            if (excelImporter == null)
            {
                throw new ArgumentNullException("excelImporter");
            }

            this.excelImporter = excelImporter;
        }

        public ActionResult Index()
        {
           return View(new ExcelImportViewModel());
        }

        [HttpPost]
        public ActionResult ImportAsync(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return ErrorView("You did not select a file for upload.");
            }

            if (file.ContentLength <= 0)
            {
                return ErrorView("The file you uploaded was empty.");
            }

            if (!IsValidFileExtension(file.FileName))
            {
                return ErrorView("The file you uploaded does not appear to be an Excel file.");
            }

            // Process the file
            ImportAsync(file.InputStream);
            
            return RedirectToAction("Index", "Dashboard");
        }

        private ActionResult ErrorView(string message)
        {
            ViewBag.Error = message;
            return View("Index", new ExcelImportViewModel());
        }

        private bool IsValidFileExtension(string fileName)
        {
            return fileName.EndsWith(ExcelFileExtension_OldFormat) || fileName.EndsWith(ExcelFileExtension_NewFormat);
        }

        private void ImportAsync(Stream file)
        {
            AsyncManager.OutstandingOperations.Increment();
            excelImporter.ImportCompleted += (s, e) =>
            {
                AsyncManager.Parameters["members"] = e.ImportedData;
                AsyncManager.OutstandingOperations.Decrement();
            };

            excelImporter.ImportFailed += (s, e) =>
            {
                AsyncManager.Parameters["exception"] = e.Exception;
                AsyncManager.OutstandingOperations.Decrement();
            };

            excelImporter.Import(file);
        }

        public ActionResult ImportCompleted(IEnumerable<IDictionary<string, object>> data)
        {
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
