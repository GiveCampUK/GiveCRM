﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GiveCRM.BusinessLogic.ExcelImport;
using GiveCRM.ImportExport;
using GiveCRM.Web.Models;

namespace GiveCRM.Web.Controllers
{


    public class ExcelImportController : AsyncController
    {
        private const string ExcelFileExtensionOldFormat = ".xls";
        private const string ExcelFileExtensionNewFormat = ".xlsx";

        private readonly IExcelImportService excelImporter;
        
        public ExcelImportController(IExcelImportService excelImporter)
        {
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
            Task.Factory.StartNew(() => ImportAsync(file.FileName, file.InputStream));
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
            return IsOldExcelFormat(fileName) || IsNewExcelFormat(fileName);
        }

        private static bool IsOldExcelFormat(string fileName)
        {
            return fileName.EndsWith(ExcelFileExtensionOldFormat);
        }

        private static bool IsNewExcelFormat(string fileName)
        {
            return fileName.EndsWith(ExcelFileExtensionNewFormat);
        }

        private void ImportAsync(string fileName, Stream fileStream)
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

            var fileType = IsNewExcelFormat(fileName) ? ExcelFileType.XLSX : ExcelFileType.XLS;
            this.excelImporter.Import(fileType, fileStream);
        }
    }
}
