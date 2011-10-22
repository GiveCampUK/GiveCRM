using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace GiveCRM.ImportExport.Test
{
    [TestFixture]
    public class When_reading_data_as_key_value_pairs
    {
        private string _testFileXls = Directory.GetCurrentDirectory() + "/TestData/MemberData.xls";
        private string _testFileXlsx = Directory.GetCurrentDirectory() + "/TestData/MemberData.xlsx";

        private ExcelImport import;

        [SetUp]
        public void SetUp()
        {
            import = new ExcelImport();
        }

        [Test]
        public void Should_throw_InvalidOperationException_if_hasHeaderRow_is_false_for_XLS()
        {
            import = new ExcelImport();

            using (FileStream stream = new FileStream(_testFileXls, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream, ExcelFileType.XLS, false);

                Assert.Throws<InvalidOperationException>(() => import.GetRowsAsKeyValuePairs(1));
            }
        }

        [Test]
        public void Should_throw_InvalidOperationException_if_hasHeaderRow_is_false_for_XLSX()
        {
            import = new ExcelImport();

            using (FileStream stream = new FileStream(_testFileXlsx, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream, ExcelFileType.XLSX, false);

                Assert.Throws<InvalidOperationException>(() => import.GetRowsAsKeyValuePairs(1));
            }
        }

        [Test]
        public void Should_return_keyvaluepairs_for_xls()
        {
            using (FileStream stream = new FileStream(_testFileXls, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream, ExcelFileType.XLS, true);

                var values = import.GetRowsAsKeyValuePairs(0);

                Assert.That(values, Is.Not.Empty);
            }            
        }

        [Test]
        public void Should_return_5_sets_of_keyvaluepairs_for_xls()
        {
            using (FileStream stream = new FileStream(_testFileXls, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream, ExcelFileType.XLS, true);

                var values = import.GetRowsAsKeyValuePairs(0);

                Assert.AreEqual(5,values.Count());
            }
        }

        [Test]
        public void Should_return_expected_values_for_first_keyvaluepair_set()
        {
            Dictionary<string, object> expected = new Dictionary<string, object>
                                                      {
                                                          {"ID", "1"},
                                                          {"Title","Mr"},
                                                          {"FirstName", "Bob"},
                                                          {"LastName","Smith"},
                                                          {"Salutation","Bob"},
                                                          {"EmailAddress","bob@give.com"},
                                                          {"AddressLine1", "1 Short Street"},
                                                          {"AddressLine2", ""},
                                                          {"City","London"},
                                                          {"Region","SouthEast"},
                                                          {"PostalCode","W1 1QP"},
                                                          {"Country","United Kingdom"},
                                                          {"HomePhone","01123 1234356"},
                                                          {"WorkPhone","01456123456"},
                                                          {"Mobile","07757123456"},
                                                          {"ContactByPhone","Y"},
                                                          {"ByEmail","Y"},
                                                          {"ContactBySMS","Y"},
                                                          {"ContactByPost","N"}
                                                      };

            IList<IDictionary<string, object>> resultSet;

            using (FileStream stream = new FileStream(_testFileXls, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream, ExcelFileType.XLS, true);

                resultSet = import.GetRowsAsKeyValuePairs(0).ToList();
            }

            CollectionAssert.AreEquivalent(expected, resultSet[0]);
        }


        [Test]
        public void Should_return_expected_values_for_last_keyvaluepair_set()
        {
            Dictionary<string, object> expected = new Dictionary<string, object>
                                                      {
                                                          {"ID", "5"},
                                                          {"Title","Mr"},
                                                          {"FirstName", "Brian"},
                                                          {"LastName","Monroe"},
                                                          {"Salutation","Brian"},
                                                          {"EmailAddress","brian@give.com"},
                                                          {"AddressLine1", "5 Short Street"},
                                                          {"AddressLine2", ""},
                                                          {"City","London"},
                                                          {"Region","SouthEast"},
                                                          {"PostalCode","W1 5QP"},
                                                          {"Country","United Kingdom"},
                                                          {"HomePhone","01123 444356"},
                                                          {"WorkPhone","01456123321"},
                                                          {"Mobile","07757123987"},
                                                          {"ContactByPhone","Y"},
                                                          {"ByEmail","N"},
                                                          {"ContactBySMS","N"},
                                                          {"ContactByPost","N"}
                                                      };

            IList<IDictionary<string, object>> resultSet;

            using (FileStream stream = new FileStream(_testFileXls, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream, ExcelFileType.XLS, true);

                resultSet = import.GetRowsAsKeyValuePairs(0).ToList();
            }

            CollectionAssert.AreEquivalent(expected, resultSet[4]);
        }
    }
}