using System.IO;
using System.Linq;
using NUnit.Framework;

namespace GiveCRM.ImportExport.Test
{
    [TestFixture]
    public class When_reading_data_from_Xls
    {
        private string _testFileXls = Directory.GetCurrentDirectory() + "/TestData/MemberData.xls";
        private string _testDataTypesXls = Directory.GetCurrentDirectory() + "/TestData/TestDataTypes.xls";

        [Test]
        public void Should_return_correct_number_of_rows_ignoring_header()
        {
            var import = new ExcelImport();

            using (FileStream stream = new FileStream(_testFileXls, FileMode.Open, FileAccess.Read))
            {
                import.OpenXls(stream);

                var rows = import.GetRows(0, true);
                Assert.AreEqual(5, rows.Count());
            }
        }

        [Test]
        public void Should_return_correct_number_of_rows_with_header()
        {
            var import = new ExcelImport();

            using (FileStream stream = new FileStream(_testFileXls, FileMode.Open, FileAccess.Read))
            {
                import.OpenXls(stream);

                var rows = import.GetRows(0, false);
                Assert.AreEqual(6, rows.Count());
            }
        }

        [Test]
        public void Should_return_correct_data()
        {
            var import = new ExcelImport();

            using (FileStream stream = new FileStream(_testFileXls, FileMode.Open, FileAccess.Read))
            {
                import.OpenXls(stream);

                var firstRow = import.GetRows(0, true).ToList()[0].ToList();
                Assert.AreEqual("Bob", firstRow[2]);
                Assert.AreEqual("01123 1234356", firstRow[12]);

                var lastRow = import.GetRows(0, true).ToList()[4].ToList();
                Assert.AreEqual("Monroe", lastRow[3]);
                Assert.AreEqual("N", lastRow[18]);
            }
        }

        [Test]
        public void Should_correctly_convert_data_type_to_string()
        {
            var import = new ExcelImport();

            using (FileStream stream = new FileStream(_testDataTypesXls, FileMode.Open, FileAccess.Read))
            {
                import.OpenXls(stream);

                var row = import.GetRows(0, false).ToList()[0].ToList();
                Assert.AreEqual("abcdef", row[0]);
                Assert.AreEqual("12345", row[2]);
                Assert.AreEqual("123.45", row[3]);
                Assert.AreEqual("0778788990", row[4]);
                Assert.AreEqual("1.99", row[5]);
                Assert.AreEqual("0.5025", row[6]);
            }
        }

        [Test]
        [Culture("en-GB")]
        public void Should_correctly_convert_date_type_to_string()
        {
            var import = new ExcelImport();

            using (FileStream stream = new FileStream(_testDataTypesXls, FileMode.Open, FileAccess.Read))
            {
                import.OpenXls(stream);

                var row = import.GetRows(0, false).ToList()[0].ToList();
                Assert.AreEqual("22/10/2011", row[1]);
            }
        }
    }
}
