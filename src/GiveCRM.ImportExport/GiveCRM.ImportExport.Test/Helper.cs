namespace GiveCRM.ImportExport.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using NUnit.Framework;
    using Cell = GiveCRM.ImportExport.Cells.Cell;

    public static class Helper
    {
        public static Workbook CreateWorkBookWithSheet()
        {
            Workbook workbook = new HSSFWorkbook();
            workbook.CreateSheet();

            return workbook;
        }

        public static Row GetRowData(ExcelExport sut, int rowIndex)
        {
            using (MemoryStream output = new MemoryStream())
            {
                sut.ExportToStream(output);

                HSSFWorkbook workbook = new HSSFWorkbook(output);

                return workbook.GetSheetAt(0).GetRow(rowIndex);
            }
        }

        public static Row GetRowData(Workbook workbook, int rowIndex)
        {
            return workbook.GetSheetAt(0).GetRow(rowIndex);
        }

        public static void CompareCellData(Row row, List<Cell> expected)
        {
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Value, row.GetCell(i).StringCellValue);
            }
        }


        public static void CompareRow(Row row, Action assert)
        {
            var enumerator = row.GetCellEnumerator();

            while (enumerator.MoveNext())
            {
                assert();
            }
        }
    }
}