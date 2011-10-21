using System;
using System.Collections.Generic;
using ExcelExport.Cells;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace ExcelExport.Formatters
{
    public class BasicFormatter: IFormatter
    {
        private  Sheet _sheet;

        public void WriteDataToSheet(Sheet sheet, IEnumerable<IEnumerable<Cell>> rowData)
        {
            if (sheet == null) throw new ArgumentNullException("sheet");
            _sheet = sheet;

            if (rowData != null)
            {
                CreateRows(rowData);
            }
        }

        private void CreateRows(IEnumerable<IEnumerable<Cell>> rowData)
        {
            int rowOrdinal = GetNextRowIndex();

            foreach (var row in rowData)
            {
                Row currentRow = _sheet.CreateRow(rowOrdinal);
                CreateRow(row, currentRow);

                rowOrdinal++;
            }
        }

        private void CreateRow(IEnumerable<Cell> row, Row currentRow)
        {
            int columnOrdinal = 0;
            
            foreach (var cell in row)
            {
                if (cell.ColumnSpan > 1)
                {
                    int rangeStartColumn = columnOrdinal;

                    for (int i = 0; i < cell.ColumnSpan; i++)
                    {
                        Cell current = CreateCell(cell, currentRow, columnOrdinal);
                        if(i == 0) current.SetCellValue(cell.Value);
                        columnOrdinal++;
                    }

                    var cra = new CellRangeAddress(currentRow.RowNum, currentRow.RowNum, rangeStartColumn,
                                                   rangeStartColumn + (cell.ColumnSpan - 1));
                    _sheet.AddMergedRegion(cra);
                }
                else
                {
                    CreateCell(cell, currentRow, columnOrdinal).SetCellValue(cell.Value);
                    columnOrdinal++;
                }
            }
        }

        private Cell CreateCell(Cell cell, Row currentRow, int columnOrdinal)
        {
            return currentRow.CreateCell(columnOrdinal, CellType.STRING);
        }

        private int GetNextRowIndex()
        {
            if (_sheet.PhysicalNumberOfRows == 0)
            {
                return 0;
            }
            return _sheet.PhysicalNumberOfRows;
        }
    }
}