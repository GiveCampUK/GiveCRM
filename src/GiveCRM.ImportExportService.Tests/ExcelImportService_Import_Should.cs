using System;
using System.Collections.Generic;
using System.IO;
using GiveCRM.ImportExport;
using Moq;
using NUnit.Framework;

namespace GiveCRM.Admin.BusinessLogic.Tests
{
    [TestFixture]
    public class ExcelImportService_Import_Should
    {
        [Test]
        [Ignore("The Importer code is in the wrong place - it should be in the main app.")]
        public void FiresImportCompletedWhenEverythingsFine()
        {
            var dataToImport = new List<IDictionary<string, object>>
                                   {
                                       new Dictionary<string, object>
                                           {
                                               {"TwitterHandle", "@joebloggs"},
                                               {"FavouriteComputer", "MacBook Air"}
                                           }
                                   };
            bool eventFired = false;
            ExcelImportService importer = SetupImportService(dataToImport, (s,e) => eventFired = true);
            var inputStream = new Mock<Stream>();

            importer.Import(inputStream.Object);

            Assert.IsTrue(eventFired);
        }

        [Test]
        [Ignore("The Importer code is in the wrong place - it should be in the main app.")]
        public void FiresImportFailedWhenSomethingGoesWrong()
        {
            bool eventFired = false;
            ExcelImportService importer = SetupImportService(new DataFormatException(), (s,e) => eventFired = true);
            var inputStream = new Mock<Stream>();

            importer.Import(inputStream.Object);

            Assert.IsTrue(eventFired);
        }

        private ExcelImportService SetupImportService(IEnumerable<IDictionary<string, object>> dataToImport, Action<object, ImportDataCompletedEventArgs> eventHandler = null)
        {
            var excelImporter = new Mock<IExcelImport>();
            excelImporter.Setup(i => i.GetRowsAsKeyValuePairs(0)).Returns(dataToImport);

            var importService = new ExcelImportService(excelImporter.Object);
            importService.ImportCompleted += eventHandler;

            return importService;
        }

        private ExcelImportService SetupImportService(Exception exception, Action<object, ImportDataFailedEventArgs> eventHandler)
        {
            var excelImporter = new Mock<IExcelImport>();
            excelImporter.Setup(i => i.GetRowsAsKeyValuePairs(0)).Throws(exception);

            var importService = new ExcelImportService(excelImporter.Object);
            importService.ImportFailed += eventHandler;

            return importService;
        }
    }
}
