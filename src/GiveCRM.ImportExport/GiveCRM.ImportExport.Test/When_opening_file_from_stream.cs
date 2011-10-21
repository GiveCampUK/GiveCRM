using System;
using NUnit.Framework;

namespace GiveCRM.ImportExport.Test
{
    [TestFixture]
    public class When_opening_file_from_stream
    {
        [Test]
        public void Should_throw_exception_if_file_stream_null()
        {
            var import = new ExcelImport();

            Assert.Throws<ArgumentNullException>(() => import.Open(null));
        }
    }
}