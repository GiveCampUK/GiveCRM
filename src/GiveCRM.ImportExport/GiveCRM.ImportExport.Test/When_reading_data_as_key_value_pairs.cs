using System;
using System.IO;
using NUnit.Framework;

namespace GiveCRM.ImportExport.Test
{
    [TestFixture]
    public class When_reading_data_as_key_value_pairs
    {
        private string _testFileXls = Directory.GetCurrentDirectory() + "/TestData/MemberData.xls";
        private string _testFileXlsx = Directory.GetCurrentDirectory() + "/TestData/MemberData.xlsx";

        [Test]
        public void Should_throw_InvalidOperationException_if_hasHeaderRow_is_false_for_XLS()
        {
            var import = new ExcelImport(ExcelFileType.XLS, false);

            using (FileStream stream = new FileStream(_testFileXls, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream);

                Assert.Throws<InvalidOperationException>(() => import.GetRowsAsKeyValuePairs(1));
            }
        }

        [Test]
        public void Should_throw_InvalidOperationException_if_hasHeaderRow_is_false_for_XLSX()
        {
            var import = new ExcelImport(ExcelFileType.XLSX, false);

            using (FileStream stream = new FileStream(_testFileXlsx, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream);

                Assert.Throws<InvalidOperationException>(() => import.GetRowsAsKeyValuePairs(1));
            }
        }

        [Test]
        public void Should_return_keyvaluepairs_for_xls()
        {
            var import = new ExcelImport(ExcelFileType.XLS, true);

            using (FileStream stream = new FileStream(_testFileXls, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream);

                var values = import.GetRowsAsKeyValuePairs(0);

                Assert.That(values, Is.Not.Empty);
            }            
        }

        [Test]
        public void Should_return_keyvaluepairs_for_xlsx()
        {
            var import = new ExcelImport(ExcelFileType.XLSX, true);

            using (FileStream stream = new FileStream(_testFileXlsx, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream);

                var values = import.GetRowsAsKeyValuePairs(0);

                Assert.That(values, Is.Not.Empty);
            }
        }
    }
}