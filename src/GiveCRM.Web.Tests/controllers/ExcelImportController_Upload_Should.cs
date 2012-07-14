namespace GiveCRM.Admin.Web.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using GiveCRM.BusinessLogic.ExcelImport;
    using GiveCRM.Web.Controllers;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class ExcelImportController_Upload_Should
    {
        private ExcelImportController controller;
        
        private const string MemberControllerName = "Member";
        private const string IndexActionName = "Index";

        private const int ZeroFileLength = 0;
        private const int NonZeroFileLength = 4096;

        [SetUp]
        public void SetUp()
        {
            var excelImporter = Substitute.For<IExcelImportService>();
            controller = new ExcelImportController(excelImporter);
        }

        [Test]
        public void ReturnToIndexView_WhenNoFileIsSelectedForUpload()
        {
            var result = controller.ImportAsync(null) as ViewResult;
            Assert.AreEqual(IndexActionName, result.ViewName);
        }

        [Test]
        public void DisplayAnErrorMesage_WhenNoFileIsUploaded()
        {
            var result = controller.ImportAsync(null) as ViewResult;
            Assert.IsNotNull(result.ViewBag.Error);
        }

        [Test]
        public void ReturnToIndexView_WhenAnEmptyFileIsUploaded()
        {
            var file = Substitute.For<HttpPostedFileBase>();
            file.ContentLength.Returns(ZeroFileLength);

            var result = controller.ImportAsync(file) as ViewResult;

            Assert.AreEqual(IndexActionName, result.ViewName);
        }

        [Test]
        public void DisplayAnErrorMessage_WhenAnEmptyFileIsUploaded()
        {
            var file = Substitute.For<HttpPostedFileBase>();
            file.ContentLength.Returns(ZeroFileLength);

            var result = controller.ImportAsync(file) as ViewResult;

            Assert.IsNotNull(result.ViewBag.Error);
        }

        [Test]
        public void ReturnToIndexView_WhenANonExcelFileIsUploaded()
        {
            var file = Substitute.For<HttpPostedFileBase>();
            file.ContentLength.Returns(NonZeroFileLength);
            file.FileName.Returns("A Text File.txt");

            var result = controller.ImportAsync(file) as ViewResult;

            Assert.AreEqual(IndexActionName, result.ViewName);
        }

        [Test]
        public void DisplayAnErrorMessage_WhenANonExcelFileIsUploaded()
        {
            var file = Substitute.For<HttpPostedFileBase>();
            file.ContentLength.Returns(NonZeroFileLength);
            file.FileName.Returns("A Text File.txt");

            var result = controller.ImportAsync(file) as ViewResult;

            Assert.IsNotNull(result.ViewBag.Error);
        }

        [Test]
        public void NotReturnAViewResult_WhenAnExcelFileIsUploaded_OldFormat()
        {
            var file = Substitute.For<HttpPostedFileBase>();
            file.ContentLength.Returns(NonZeroFileLength);
            file.FileName.Returns("An old-format Excel file.xls");

            var result = controller.ImportAsync(file);

            Assert.IsNotInstanceOf<ViewResult>(result);
        }

        [Test]
        public void NotReturnAViewResult_WhenAnExcelFileIsUploaded_NewFormat()
        {
            var file = Substitute.For<HttpPostedFileBase>();
            file.ContentLength.Returns(NonZeroFileLength);
            file.FileName.Returns("A new-format Excel file.xlsx");

            var result = controller.ImportAsync(file);

            Assert.IsNotInstanceOf<ViewResult>(result);
        }

        [Test]
        public void RedirectToDashboardIndex_WhenFileIsValidForImport()
        {
            var file = Substitute.For<HttpPostedFileBase>();
            file.ContentLength.Returns(NonZeroFileLength);
            file.FileName.Returns("Valid file.xls");

            var result = controller.ImportAsync(file) as RedirectToRouteResult;

            Assert.AreEqual(MemberControllerName, result.RouteValues["controller"]);
            Assert.AreEqual(IndexActionName, result.RouteValues["action"]);
        }

        [Test]
        public void RedirectToDashboardIndex_WhenFileImportIsComplete()
        {
            var result = controller.ImportCompleted(Enumerable.Empty<IDictionary<string, object>>()) as RedirectToRouteResult;

            Assert.AreEqual(MemberControllerName, result.RouteValues["controller"]);
            Assert.AreEqual(IndexActionName, result.RouteValues["action"]);
        }
    }
}
