namespace GiveCRM.ImportExport.Test
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using GiveCRM.ImportExport.Borders;
    using GiveCRM.ImportExport.Cells;
    using NPOI.SS.UserModel;
    using NUnit.Framework;
    using BorderStyle = GiveCRM.ImportExport.Borders.BorderStyle;
    using Cell = GiveCRM.ImportExport.Cells.Cell;

    [TestFixture]
    public class When_creating_formatted_content
    {
        private List<List<Cell>> sampleData;
        private CellFormatter target;
        private Workbook workBook;

        [SetUp]
        public void Setup()
        {
            target = new CellFormatter();
            workBook = Helper.CreateWorkBookWithSheet();

            sampleData = new List<List<Cell>> 
                              {
                                  new List<Cell>
                                  {
                                    new Cell {Value = "1"},
                                    new Cell {Value = "2", IsBold = true},
                                    new Cell {Value = "a"},
                                    new Cell {Value = "b", IsBold = true},
                                  }
                              };

        }

        [Test]
        public void Should_be_able_to_create_formatter()
        {
            Assert.IsNotNull(target);
        }
        
        [Test]
        public void Should_be_able_to_export_plain_text_from_formatted_data()
        {
            target.WriteDataToSheet(workBook.GetSheetAt(0), sampleData);

            Row row = Helper.GetRowData(workBook, 0);
            Helper.CompareCellData(row, sampleData[0].Cast<Cell>().ToList());      

        }

        [Test]
        public void Should_be_able_to_export_bold_text_from_formatted_data()
        {
            target.WriteDataToSheet(workBook.GetSheetAt(0), sampleData);

            Row excelRow = Helper.GetRowData(workBook, 0);
            for (int i = 0; i < sampleData[0].Count; i++)
            {
                var boldWeight = excelRow.GetCell(i).CellStyle.GetFont(workBook).Boldweight;
                Assert.IsTrue(sampleData[0][i].IsBold ? boldWeight == (short)FontBoldWeight.BOLD : boldWeight == (short)FontBoldWeight.NORMAL );                
            }
        }

        [Test]
        public void Should_be_able_to_set_background_color_of_cell()
        {
            sampleData[0][0].BackgroundColor = Color.Gray;
            target.WriteDataToSheet(workBook.GetSheetAt(0), sampleData);

            Row excelRow = Helper.GetRowData(workBook, 0);
            Assert.IsTrue(excelRow.GetCell(0).CellStyle.FillForegroundColor != 64);
        }

        [Test]
        public void Background_color_of_cell_should_be_correct_NPOI_color()
        {
            sampleData[0][0].BackgroundColor = Color.Gray;

            target.WriteDataToSheet(workBook.GetSheetAt(0), sampleData);

            Row excelRow = Helper.GetRowData(workBook, 0);
            Assert.AreEqual(IndexedColors.GREY_40_PERCENT.Index, excelRow.GetCell(0).CellStyle.FillForegroundColor);
        }

        [Test]
        public void Should_be_able_to_set_top_border_on_a_cell()
        {
            sampleData[0][0].Borders.Add(new Border {Location = BorderLocation.Top, Style = BorderStyle.Thin});

            target.WriteDataToSheet(workBook.GetSheetAt(0), sampleData);

            Row excelRow = Helper.GetRowData(workBook, 0);
            Assert.AreEqual(CellBorderType.THIN, excelRow.GetCell(0).CellStyle.BorderTop);
            Assert.AreEqual(CellBorderType.NONE, excelRow.GetCell(0).CellStyle.BorderBottom);
            Assert.AreEqual(CellBorderType.NONE, excelRow.GetCell(0).CellStyle.BorderLeft);
            Assert.AreEqual(CellBorderType.NONE, excelRow.GetCell(0).CellStyle.BorderRight);
        }

        [Test]
        public void Should_be_able_to_set_bottom_border_on_a_cell()
        {
            sampleData[0][0].Borders.Add(new Border {Location = BorderLocation.Bottom, Style = BorderStyle.Thin});

            target.WriteDataToSheet(workBook.GetSheetAt(0), sampleData);

            Row excelRow = Helper.GetRowData(workBook, 0);
            Assert.AreEqual(CellBorderType.THIN, excelRow.GetCell(0).CellStyle.BorderBottom);
            Assert.AreEqual(CellBorderType.NONE, excelRow.GetCell(0).CellStyle.BorderTop);
            Assert.AreEqual(CellBorderType.NONE, excelRow.GetCell(0).CellStyle.BorderLeft);
            Assert.AreEqual(CellBorderType.NONE, excelRow.GetCell(0).CellStyle.BorderRight);
        }

        [Test]
        public void Should_be_able_to_set_left_border_on_a_cell()
        {
            sampleData[0][0].Borders.Add(new Border { Location = BorderLocation.Left, Style = BorderStyle.Thin });

            target.WriteDataToSheet(workBook.GetSheetAt(0), sampleData);

            Row excelRow = Helper.GetRowData(workBook, 0);
            Assert.AreEqual(CellBorderType.NONE, excelRow.GetCell(0).CellStyle.BorderBottom);
            Assert.AreEqual(CellBorderType.NONE, excelRow.GetCell(0).CellStyle.BorderTop);
            Assert.AreEqual(CellBorderType.THIN, excelRow.GetCell(0).CellStyle.BorderLeft);
            Assert.AreEqual(CellBorderType.NONE, excelRow.GetCell(0).CellStyle.BorderRight);
        }

        [Test]
        public void Should_be_able_to_set_right_border_on_a_cell()
        {
            sampleData[0][0].Borders.Add(new Border { Location = BorderLocation.Right, Style = BorderStyle.Thin });

            target.WriteDataToSheet(workBook.GetSheetAt(0), sampleData);

            Row excelRow = Helper.GetRowData(workBook, 0);
            Assert.AreEqual(CellBorderType.NONE, excelRow.GetCell(0).CellStyle.BorderBottom);
            Assert.AreEqual(CellBorderType.NONE, excelRow.GetCell(0).CellStyle.BorderTop);
            Assert.AreEqual(CellBorderType.NONE, excelRow.GetCell(0).CellStyle.BorderLeft);
            Assert.AreEqual(CellBorderType.THIN, excelRow.GetCell(0).CellStyle.BorderRight);
        }

        [Test]
        public void Should_be_able_to_set_all_borders_on_a_cell()
        {
            List<Border> borders = new List<Border>
                                       {
                                           new Border {Location = BorderLocation.Top, Style = BorderStyle.Thin},
                                           new Border {Location = BorderLocation.Right, Style = BorderStyle.Thin},
                                           new Border {Location = BorderLocation.Left, Style = BorderStyle.Thin},
                                           new Border {Location = BorderLocation.Bottom, Style = BorderStyle.Thin}
                                       };

            sampleData[0][0].Borders.AddRange(borders);

            target.WriteDataToSheet(workBook.GetSheetAt(0), sampleData);

            Row excelRow = Helper.GetRowData(workBook, 0);
            Assert.AreEqual(CellBorderType.THIN, excelRow.GetCell(0).CellStyle.BorderBottom);
            Assert.AreEqual(CellBorderType.THIN, excelRow.GetCell(0).CellStyle.BorderTop);
            Assert.AreEqual(CellBorderType.THIN, excelRow.GetCell(0).CellStyle.BorderLeft);
            Assert.AreEqual(CellBorderType.THIN, excelRow.GetCell(0).CellStyle.BorderRight);
        }


        [Test]
        public void Should_be_able_to_set_color_border_on_a_cell()
        {
            sampleData[0][0].Borders.Add(new Border { Location = BorderLocation.Top, Style = BorderStyle.Thin, Color = Color.Red});

            target.WriteDataToSheet(workBook.GetSheetAt(0), sampleData);

            Row excelRow = Helper.GetRowData(workBook, 0);
            Assert.AreEqual(IndexedColors.RED.Index, excelRow.GetCell(0).CellStyle.TopBorderColor);

        }
    }
}