using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace GiveCRM.ImportExport.Test
{
    [TestFixture]
    public class When_reading_data_as_key_value_pairs_from_Xls
    {
        private string _testFileXls = Directory.GetCurrentDirectory() + "/TestData/MemberData.xls";

        private ExcelImport import;
        private Dictionary<string, object> _firstRowDictionary;
        private Dictionary<string, object> _lastRowDictionary;

        [SetUp]
        public void SetUp()
        {
            _firstRowDictionary = new Dictionary<string, object>
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
                                                          {"ContactByEmail","Y"},
                                                          {"ContactBySMS","Y"},
                                                          {"ContactByPost","N"}
                                                      };

            _lastRowDictionary = new Dictionary<string, object>
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
                                                          {"ContactByEmail","N"},
                                                          {"ContactBySMS","N"},
                                                          {"ContactByPost","N"}
                                                      };

            import = new ExcelImport(ExcelFileType.XLS, true);
        }

        [Test]
        public void Should_throw_InvalidOperationException_if_hasHeaderRow_is_false_for_XLS()
        {
            import = new ExcelImport(ExcelFileType.XLS, false);

            using (FileStream stream = new FileStream(_testFileXls, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream);

                Assert.Throws<InvalidOperationException>(() => import.GetRowsAsKeyValuePairs(1));
            }
        }

        [Test]
        public void Should_return_keyvaluepairs_for_xls()
        {
            using (FileStream stream = new FileStream(_testFileXls, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream);

                var values = import.GetRowsAsKeyValuePairs(0);

                Assert.That(values, Is.Not.Empty);
            }            
        }
        
        [Test]
        public void Should_return_5_sets_of_keyvaluepairs_for_xls()
        {
            using (FileStream stream = new FileStream(_testFileXls, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream);

                var values = import.GetRowsAsKeyValuePairs(0);
                Assert.AreEqual(5,values.Count());
            }
        }

        [Test]
        public void Should_return_expected_values_for_first_keyvaluepair_set_for_Xls()
        {
            IList<IDictionary<string, object>> resultSet;

            using (FileStream stream = new FileStream(_testFileXls, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream);

                resultSet = import.GetRowsAsKeyValuePairs(0).ToList();
            }

            CollectionAssert.AreEquivalent(_firstRowDictionary, resultSet[0]);
        }


        [Test]
        public void Should_return_expected_values_for_last_keyvaluepair_set_for_Xls()
        {
            IList<IDictionary<string, object>> resultSet;

            using (FileStream stream = new FileStream(_testFileXls, FileMode.Open, FileAccess.Read))
            {
                import.Open(stream);

                resultSet = import.GetRowsAsKeyValuePairs(0).ToList();
            }

            CollectionAssert.AreEquivalent(_lastRowDictionary, resultSet[4]);
        }
    }
}