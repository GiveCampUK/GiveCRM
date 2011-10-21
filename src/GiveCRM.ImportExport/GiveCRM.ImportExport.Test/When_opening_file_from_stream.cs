using System;
using System.Linq;
using System.IO;
using System.Net.Mime;
using NUnit.Framework;

namespace GiveCRM.ImportExport.Test
{
    [TestFixture]
    public class When_opening_file_from_stream
    {
        private string _testFile = Directory.GetCurrentDirectory() + "/TestData/MemberData.xlsx";
        private string _testFile97 = Directory.GetCurrentDirectory() + "/TestData/MemberData97.xls";

        [Test]
        public void Should_throw_exception_if_file_stream_null()
        {
            var import = new ExcelImport();

            Assert.Throws<ArgumentNullException>(() => import.OpenXlsx(null));
        }

        [Test]
        public void Should_accept_file_stream()
        {
            var import = new ExcelImport();
            
            using (FileStream stream = new FileStream(_testFile, FileMode.Open, FileAccess.Read))
            {
                import.OpenXlsx(stream);
            }
        }

        [Test]
        public void Should_accept_file_stream_from_Excel97()
        {
            var import = new ExcelImport();

            using (FileStream stream = new FileStream(_testFile97, FileMode.Open, FileAccess.Read))
            {
                import.OpenXls(stream);
            }
        }

        [Test]
        public void Should_return_row_data_from_Excel97()
        {
            var import = new ExcelImport();

            using (FileStream stream = new FileStream(_testFile97, FileMode.Open, FileAccess.Read))
            {
                import.OpenXls(stream);

                var rows = import.GetRows(0);
                Assert.AreEqual(5, rows.Count());
            }
        }
    }
}