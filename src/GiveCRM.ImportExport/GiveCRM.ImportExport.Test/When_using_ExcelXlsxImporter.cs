using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GiveCRM.ImportExport.Test
{
    [TestFixture]
    public class When_using_ExcelXlsxImporter
    {
        private string testFileXlsx = Directory.GetCurrentDirectory() + "/TestData/MemberData.xlsx";

        [Test]
        public void Should_throw_exception_if_file_stream_null()
        {
            var importer = new ExcelXlsxImporter();

            Assert.Throws<ArgumentNullException>(() => importer.Open(null));
        }

        [Test]
        public void Should_set_package_from_stream()
        {
            using (FileStream stream = new FileStream(testFileXlsx, FileMode.Open, FileAccess.Read))
            {
                var importer = new ExcelXlsxImporter();
                importer.Open(stream);

                Assert.IsNotNull(importer.Package);
            }
        }

        [Test]
        public void Should_set_workbook_from_stream()
        {
            using (FileStream stream = new FileStream(testFileXlsx, FileMode.Open, FileAccess.Read))
            {
                var importer = new ExcelXlsxImporter();
                importer.Open(stream);

                Assert.IsNotNull(importer.Workbook);
            }
        }
    }
}
