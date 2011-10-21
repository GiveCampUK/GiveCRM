using System.IO;
using GiveCRM.ImportExport.Cells;
using NPOI.SS.UserModel;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using Cell = GiveCRM.ImportExport.Cells.Cell;

namespace GiveCRM.ImportExport.Test
{
    [TestFixture]
    public class When_using_ExcelExport
    {
        [Test]
        public void Should_be_able_to_create_instance_of_class()
        {
            ExcelExport target = new ExcelExport();

            Assert.IsNotNull(target);
        }

        [Test]
        public void Should_be_able_to_pass_data_to_be_written_to_workbook()
        {
            ExcelExport target = new ExcelExport();
            Workbook book = Helper.CreateWorkBookWithSheet();

            List<List<Cell>> cells = new List<List<Cell>>
                                         {
                                             new List<Cell>
                                                 {
                                                     new Cell {Value = "123"}
                                                 }
                                         };

            CellFormatter formatter = Substitute.For<CellFormatter>();
            formatter.WriteDataToSheet(book.GetSheetAt(0), cells);
            formatter.Received().WriteDataToSheet(book.GetSheetAt(0), cells);
        }

        [Test]
        public void Should_return_stream_with_data()
        {
            MemoryStream outputStream = new MemoryStream();
            ExcelExport target = new ExcelExport();

            target.ExportToStream(outputStream);

            Assert.IsTrue(outputStream.Length > 0);
        }
    }
}