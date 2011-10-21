using System.Collections.Generic;
using GiveCRM.ImportExport.Cells;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NUnit.Framework;
using Cell = GiveCRM.ImportExport.Cells.Cell;

namespace GiveCRM.ImportExport.Test
{
    [TestFixture]
    public class When_creating_plain_content
    {
        private ExcelExport sut;

        private List<List<Cell>> _sampleData;
        private CellFormatter _formatter;
        private Workbook _workBook;

        [SetUp]
        public void Setup()
        {
            sut = new ExcelExport();
            _formatter = new CellFormatter();
            _workBook = Helper.CreateWorkBookWithSheet();

            _sampleData = new List<List<Cell>> 
                              {
                                  new List<Cell>
                                  {
                                    new Cell {Value = "1"},
                                    new Cell {Value = "2"},
                                    new Cell {Value = "a"},
                                    new Cell {Value = "b"},
                                  }
                              };

        }
        
        [Test]
        public  void Should_be_able_to_create_instance()
        {
            Assert.NotNull(sut);
        }

        [Test]
        public void Should_output_data()
        {
            _formatter.WriteDataToSheet(_workBook.GetSheetAt(0),_sampleData);

            Row row = Helper.GetRowData(_workBook, 0);
            Helper.CompareCellData(row, _sampleData[0]);            
        }
        
        [Test]
        public void Should_output_multiple_rows_of_data()
        {
            _formatter.WriteDataToSheet(_workBook.GetSheetAt(0), _sampleData);

            for (int i = 0; i < _sampleData.Count; i++)
            {
                Row row = Helper.GetRowData(_workBook, 0);
                Helper.CompareCellData(row, _sampleData[i]);
            }
        }

        [Test]
        public void Should_output_cell_that_spans_2_columns_at_the_beginning_of_the_row()
        {
            _sampleData[0][0].ColumnSpan = 2;

            _formatter.WriteDataToSheet(_workBook.GetSheetAt(0), _sampleData);

            Sheet sheet = _workBook.GetSheetAt(0);
            Row row = Helper.GetRowData(_workBook, 0);

            CellRangeAddress mergedRegion= sheet.GetMergedRegion(0);

            Assert.IsTrue(mergedRegion.FirstColumn == 0 && mergedRegion.LastColumn == 1);
            Assert.AreEqual("1", row.GetCell(0).StringCellValue);
            Assert.AreEqual("2", row.GetCell(2).StringCellValue);
        }

        [Test]
        public void Should_output_cell_that_spans_2_columns_in_the_middle_of_the_row()
        {
            _sampleData[0][1].ColumnSpan = 2;

            _formatter.WriteDataToSheet(_workBook.GetSheetAt(0), _sampleData);

            Sheet sheet = _workBook.GetSheetAt(0);
            Row row = Helper.GetRowData(_workBook, 0);

            CellRangeAddress mergedRegion = sheet.GetMergedRegion(0);

            Assert.IsTrue(mergedRegion.FirstColumn == 1 && mergedRegion.LastColumn == 2);
            Assert.AreEqual("1", row.GetCell(0).StringCellValue);
            Assert.AreEqual("2", row.GetCell(1).StringCellValue);
            Assert.AreEqual("a", row.GetCell(3).StringCellValue);
        }

        [Test]
        public void Should_output_cell_that_spans_2_columns_at_the_end_of_the_row()
        {
            _sampleData[0][3].ColumnSpan = 2;

            _formatter.WriteDataToSheet(_workBook.GetSheetAt(0), _sampleData);

            Sheet sheet = _workBook.GetSheetAt(0);
            Row row = Helper.GetRowData(_workBook, 0);

            CellRangeAddress mergedRegion = sheet.GetMergedRegion(0);

            Assert.IsTrue(mergedRegion.FirstColumn == 3 && mergedRegion.LastColumn == 4);
            Assert.AreEqual("1", row.GetCell(0).StringCellValue);
            Assert.AreEqual("2", row.GetCell(1).StringCellValue);
            Assert.AreEqual("a", row.GetCell(2).StringCellValue);
            Assert.AreEqual("b", row.GetCell(3).StringCellValue);
        }
    }
}
