using System;
using System.Collections.Generic;
using System.IO;
using GiveCRM.BusinessLogic.ExcelImport;
using GiveCRM.ImportExport;
using NSubstitute;
using NUnit.Framework;

namespace GiveCRM.BusinessLogic.Tests
{
    [TestFixture]
    public class ExcelImportService_Import_Should
    {
        [Test]
        public void FireImportCompletedWhenEverythingsFine()
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
            ExcelImportService importer = SetupSuccessfulImportService(dataToImport, (s,e) => eventFired = true);
            var inputStream = Substitute.For<Stream>();

            importer.Import(inputStream);

            Assert.IsTrue(eventFired);
        }

        [Test]
        public void FireImportFailedWhenSomethingGoesWrong()
        {
            bool eventFired = false;
            ExcelImportService importer = SetupFailingImportService(new InvalidOperationException(), (s, e) => eventFired = true);
            var inputStream = Substitute.For<Stream>();

            importer.Import(inputStream);

            Assert.IsTrue(eventFired);
        }

        private ExcelImportService SetupSuccessfulImportService(IEnumerable<IDictionary<string, object>> dataToImport, Action<object, ImportDataCompletedEventArgs> eventHandler = null)
        {
            var excelImporter = Substitute.For<IExcelImport>();
            excelImporter.GetRowsAsKeyValuePairs(0).ReturnsForAnyArgs(dataToImport);

            var importService = CreateImportService(excelImporter);
            importService.ImportCompleted += eventHandler;

            return importService;
        }

        private ExcelImportService SetupFailingImportService(Exception exception, Action<object, ImportDataFailedEventArgs> eventHandler)
        {
            var excelImporter = Substitute.For<IExcelImport>();
            excelImporter.GetRowsAsKeyValuePairs(0).ReturnsForAnyArgs(_ => { throw exception; });

            var importService = CreateImportService(excelImporter);
            importService.ImportFailed += eventHandler;

            return importService;
        }

        private static ExcelImportService CreateImportService(IExcelImport excelImporter)
        {
            var memberService = Substitute.For<IMemberService>();
            var memberFactory = Substitute.For<IMemberFactory>();

            var importService = new ExcelImportService(excelImporter, memberService, memberFactory);
            return importService;
        }
    }
}
