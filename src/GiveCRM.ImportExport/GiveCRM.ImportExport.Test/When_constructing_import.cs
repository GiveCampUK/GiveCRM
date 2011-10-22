using System;
using NUnit.Framework;

namespace GiveCRM.ImportExport.Test
{
    [TestFixture]
    public class When_constructing_import
    {
        [Test]
        public void Should_create_instance_of_import()
        {
            Assert.IsNotNull(new ExcelImport());
        }

        [Test]
        public void Should_be_disposable()
        {
            var import = new ExcelImport();

            Assert.IsTrue(import is IDisposable);
        }

        [Test]
        public void Should_dispose_instance()
        {
            using (var import = new ExcelImport())
            {
            }
        }
    }
}