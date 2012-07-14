namespace GiveCRM.ImportExport.Test
{
    using System;
    using System.IO;
    using NUnit.Framework;

    [TestFixture]
    public class When_using_ExcelXlsImporter
    {
        private string testFileXls = Directory.GetCurrentDirectory() + "/TestData/MemberData.xls";

        [Test]
        public void Should_throw_exception_if_file_stream_null()
        {
            var importer = new ExcelXlsImporter();

            Assert.Throws<ArgumentNullException>(() => importer.Open(null));
        }

        [Test]
        public void Should_set_workbook_from_stream()
        {
            using (FileStream stream = new FileStream(testFileXls, FileMode.Open, FileAccess.Read))
            {
                var importer = new ExcelXlsImporter();
                importer.Open(stream);

                Assert.IsNotNull(importer.Workbook);
            }
        }
    }
}
