
namespace GiveCRM.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using GiveCRM.Web.Models;
    using GiveCRM.Web.Services.ExcelImport;

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

        [HttpGet]
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
            Task.Factory.StartNew(() => ImportAsync(file.InputStream));
            
            return RedirectToAction("Index", "Member");
        }

        [HttpGet]
        public ActionResult ImportCompleted(IEnumerable<IDictionary<string, object>> data)
        {
            return RedirectToAction("Index", "Member");
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
            this.excelImporter.ImportCompleted += (s, e) =>
            {
                AsyncManager.Parameters["members"] = e.ImportedData;
                AsyncManager.OutstandingOperations.Decrement();
            };

            this.excelImporter.ImportFailed += (s, e) =>
            {
                AsyncManager.Parameters["exception"] = e.Exception;
                AsyncManager.OutstandingOperations.Decrement();
            };

            this.excelImporter.Import(file);
        }
    }
}
