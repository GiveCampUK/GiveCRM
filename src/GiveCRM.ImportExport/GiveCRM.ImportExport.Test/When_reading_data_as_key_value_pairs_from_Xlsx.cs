﻿namespace GiveCRM.ImportExport.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class When_reading_data_as_key_value_pairs_from_Xlsx
    {
        private string testFileXlsx = Directory.GetCurrentDirectory() + "/TestData/MemberData.xlsx";

        private ExcelImport import;
        private Dictionary<string, object> firstRowDictionary;
        private Dictionary<string, object> lastRowDictionary;

        [SetUp]
        public void SetUp()
        {
            import = new ExcelImport();

            firstRowDictionary = new Dictionary<string, object>
                                                      {
                                                          {"ID", "1"},
                                                          {"Title","Mr"},
                                                          {"FirstName", "Bob"},
                                                          {"LastName","Smith"},
                                                          {"Salutation","Bob"},
                                                          {"EmailAddress","bob@give.com"},
                                                          {"AddressLine1", "1 Short Street"},
                                                          {"AddressLine2", string.Empty},
                                                          {"City","London"},
                                                          {"Region","SouthEast"},
                                                          {"PostalCode","W1 1QP"},
                                                          {"Country","United Kingdom"},
                                                          {"HomePhone","01123 1234356"},
                                                          {"WorkPhone","01456123456"},
                                                          {"Mobile","07757123456"},
                                                          {"ContactByPhone","Y"},
                                                          {"ContactByEmail","Y"},
                                                          {"ContactBySMS","Y"},
                                                          {"ContactByPost","N"}
                                                      };

            lastRowDictionary = new Dictionary<string, object>
                                                      {
                                                          {"ID", "5"},
                                                          {"Title","Mr"},
                                                          {"FirstName", "Brian"},
                                                          {"LastName","Monroe"},
                                                          {"Salutation","Brian"},
                                                          {"EmailAddress","brian@give.com"},
                                                          {"AddressLine1", "5 Short Street"},
                                                          {"AddressLine2", string.Empty},
                                                          {"City","London"},
                                                          {"Region","SouthEast"},
                                                          {"PostalCode","W1 5QP"},
                                                          {"Country","United Kingdom"},
                                                          {"HomePhone","01123 444356"},
                                                          {"WorkPhone","01456123321"},
                                                          {"Mobile","07757123987"},
                                                          {"ContactByPhone","Y"},
                                                          {"ContactByEmail","N"},
                                                          {"ContactBySMS","N"},
                                                          {"ContactByPost","N"}
                                                      };

        }

        [Test]
        public void Should_throw_InvalidOperationException_if_hasHeaderRow_is_false_for_XLSX()
        {
            import = new ExcelImport();

            using (FileStream stream = new FileStream(testFileXlsx, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream, ExcelFileType.XLSX, false);

                Assert.Throws<InvalidOperationException>(() => import.GetRowsAsKeyValuePairs(1));
            }
        }

        [Test]
        public void Should_return_keyvaluepairs_for_xlsx()
        {
            var import = new ExcelImport();

            using (FileStream stream = new FileStream(testFileXlsx, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream, ExcelFileType.XLSX, true);

                var values = import.GetRowsAsKeyValuePairs(0);
                Assert.That(values, Is.Not.Empty);
            }
        }

        [Test]
        public void Should_return_expected_values_for_first_keyvaluepair_set_for_Xlsx()
        {
            IList<IDictionary<string, object>> resultSet;

            using (FileStream stream = new FileStream(testFileXlsx, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream, ExcelFileType.XLSX, true);

                resultSet = import.GetRowsAsKeyValuePairs(0).ToList();
            }

            CollectionAssert.AreEquivalent(firstRowDictionary, resultSet[0]);
        }


        [Test]
        public void Should_return_expected_values_for_last_keyvaluepair_set_for_Xlsx()
        {
            IList<IDictionary<string, object>> resultSet;

            using (FileStream stream = new FileStream(testFileXlsx, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream, ExcelFileType.XLSX, true);

                resultSet = import.GetRowsAsKeyValuePairs(0).ToList();
            }

            CollectionAssert.AreEquivalent(lastRowDictionary, resultSet[4]);
        }
    }
}
