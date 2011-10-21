using System;
using System.Collections.Generic;
using System.Drawing;
using GiveCRM.ImportExport.Borders;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using Font = NPOI.SS.UserModel.Font;

namespace GiveCRM.ImportExport.Cells
{
    public class CellFormatter
    {
        private Sheet _sheet;

        public virtual void WriteDataToSheet(Sheet sheet, IEnumerable<IEnumerable<Cell>> rowData)
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

        private int GetNextRowIndex()
        {
            if (_sheet.PhysicalNumberOfRows == 0)
            {
                return 0;
            }
            return _sheet.PhysicalNumberOfRows;
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
                        NPOI.SS.UserModel.Cell current = CreateCell(cell, currentRow, columnOrdinal);
                        if (i == 0) current.SetCellValue(cell.Value);
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

        private NPOI.SS.UserModel.Cell CreateCell(Cell cell, Row currentRow, int columnOrdinal)
        {
            var nCell = currentRow.CreateCell(columnOrdinal, CellType.STRING);
            nCell.CellStyle = GetCellStyle(cell);

            return nCell;
        }

        private CellStyle GetCellStyle(Cell cell)
        {
            CellStyle style = _sheet.Workbook.CreateCellStyle();

            SetBold(cell, style);

            SetColor(cell, style);

            SetBorder(cell, style);

            return style;
        }

        private void SetColor(Cell cell, CellStyle style)
        {
            if (cell.BackgroundColor != Color.Empty || cell.BackgroundColor != Color.White)
            {
                style.FillForegroundColor = cell.BackgroundColor.ToNPOIColor();
                style.FillPattern = FillPatternType.SOLID_FOREGROUND;
            }
        }

        private void SetBorder(Cell cell, CellStyle style)
        {
            foreach (var border in cell.Borders)
            {
                if (border.Location != BorderLocation.None)
                {
                    switch (border.Location)
                    {
                        case BorderLocation.Top:
                            style.BorderTop = border.Style.NPOIBorderType();
                            style.TopBorderColor = border.Color.ToNPOIColor();
                            break;

                        case BorderLocation.Bottom:
                            style.BorderBottom = border.Style.NPOIBorderType();
                            style.BottomBorderColor = border.Color.ToNPOIColor();
                            break;

                        case BorderLocation.Left:
                            style.BorderLeft = border.Style.NPOIBorderType();
                            style.LeftBorderColor = border.Color.ToNPOIColor();
                            break;

                        case BorderLocation.Right:
                            style.BorderRight = border.Style.NPOIBorderType();
                            style.RightBorderColor = border.Color.ToNPOIColor();
                            break;

                        default:
                            throw new InvalidOperationException("Trying to set border for non-existent side");

                    }
                }
            }
        }

        private void SetBold(Cell cell, CellStyle style)
        {
            if (cell.IsBold)
            {
                Font font = _sheet.Workbook.CreateFont();
                font.Boldweight = (short)FontBoldWeight.BOLD;
                style.SetFont(font);                
            }
        }
    }
}