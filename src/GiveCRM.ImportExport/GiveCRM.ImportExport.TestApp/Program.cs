using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using GiveCRM.ImportExport;
using GiveCRM.ImportExport.Borders;
using GiveCRM.ImportExport.Cells;

namespace GiveCRM.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the cell values, separated by commas.");
            Console.WriteLine("To create merged cells, add : and col span value after cell value");
            Console.WriteLine("For a new row press Enter.");
            Console.WriteLine("To exit data entry mode, press Esc.");

            var data = new List<List<Cell>>();

            string row;
            do
            {
                row = Console.ReadLine();
                if (row != null)
                {
                    var cellValues = row.Split(',');
                    var basicRow = new List<Cell>();
                    foreach (string cellValue in cellValues)
                    {
                        if (cellValue.Contains(":"))
                        {
                            string[] cellData = cellValue.Split(':');
                            string value = cellData[0];
                            string colSpanString = cellData[1];
                            int colSpan = Int32.Parse(colSpanString);

                            Border border = new Border
                                                {
                                                    Color = Color.Red,
                                                    Location = BorderLocation.Bottom,
                                                    Style = BorderStyle.Thick
                                                };
                            List<Border> borders = new List<Border>();
                            borders.Add(border);
                            basicRow.Add(new Cell()
                                             {
                                                 Value = value,
                                                 ColumnSpan = colSpan,
                                                 BackgroundColor = Color.LightGray,
                                                 Borders = borders
                                             });
                        }
                        else
                        {
                            basicRow.Add(new Cell { Value = cellValue }); 
                        }
                    }
                    data.Add(basicRow);
                }
            } while (row != String.Empty);

            var export = new ExcelExport();
            export.WriteDataToExport(data, new CellFormatter(), ExcelFileType.XLS);
            using (var stream = new FileStream(@"C:\temp\export.xls", FileMode.Create))
            {
                export.ExportToStream(stream);
            }
            Console.WriteLine(@"File exported to: C:\temp\export.xls");
            Console.ReadLine();
        }
    }
}
