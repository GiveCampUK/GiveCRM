using System;
using System.IO;
using NUnit.Framework;

namespace GiveCRM.ImportExport.Test
{
    [TestFixture]
    public class When_opening_file_from_stream
    {
        private string _testFileXlsx = Directory.GetCurrentDirectory() + "/TestData/MemberData.xlsx";
        private string _testFileXls = Directory.GetCurrentDirectory() + "/TestData/MemberData.xls";

        [Test]
        public void Should_throw_exception_if_file_stream_for_xlsx_null()
        {
            var import = new ExcelImport(ExcelFileType.XLSX, true);

            Assert.Throws<ArgumentNullException>(() => import.Open(null));
        }

        [Test]
        public void Should_throw_exception_if_file_stream_for_xls_null()
        {
            var import = new ExcelImport(ExcelFileType.XLS, true);

            Assert.Throws<ArgumentNullException>(() => import.Open(null));
        }

        [Test]
        public void Should_accept_file_stream()
        {
            var import = new ExcelImport(ExcelFileType.XLSX, true);
            
            using (FileStream stream = new FileStream(_testFileXlsx, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream);
            }
        }

        [Test]
        public void Should_accept_file_stream_from_Excel97()
        {
            var import = new ExcelImport(ExcelFileType.XLS, true);

            using (FileStream stream = new FileStream(_testFileXls, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream);
            }
        }

        [Test]
        public void Should_create_Xls_Importer_for_Xls_file()
        {
            var import = new ExcelImport(ExcelFileType.XLS, true);

            using (FileStream stream = new FileStream(_testFileXls, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream);

                Assert.IsInstanceOf(typeof(ExcelXlsImporter), import.ExcelImporter);
            }
        }

        [Test]
        public void Should_create_Xlsx_Importer_for_Xlsx_file()
        {
            var import = new ExcelImport(ExcelFileType.XLSX, true);

            using (FileStream stream = new FileStream(_testFileXlsx, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream);

                Assert.IsInstanceOf(typeof(ExcelXlsxImporter), import.ExcelImporter);
            }
        }
    }
}