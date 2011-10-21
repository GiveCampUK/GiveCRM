using System.Drawing;
using GiveCRM.ImportExport.Borders;
using GiveCRM.ImportExport.Cells;
using NUnit.Framework;

namespace GiveCRM.ImportExport.Test
{
    [TestFixture]
    public class When_constructing_export
    {
        [Test]
        public void Border_should_have_default_values()
        {
            Border target = new Border();
            Assert.AreEqual(BorderLocation.None, target.Location);
            Assert.AreEqual(BorderStyle.None, target.Style);
            Assert.AreEqual(Color.Black, target.Color);
        }
        
        [Test]
        public void Cell_should_have_default_values()
        {
            Cell target = new Cell();
            Assert.AreEqual(1, target.ColumnSpan);
        }

        [Test]
        public void RichTextCell_should_have_default_value()
        {
            Cell target = new Cell();

            Assert.IsFalse(target.IsBold);
            Assert.AreEqual(Color.Empty, target.BackgroundColor);
            Assert.IsNotNull(target.Borders);
        }

    }
}