namespace GiveCRM.ImportExport.Cells
{
    using System.Collections.Generic;
    using System.Drawing;
    using GiveCRM.ImportExport.Borders;

    public class Cell
    {
        public Cell()
        {
            Borders = new List<Border>();
            ColumnSpan = 1;
        }

        public string Value { get; set; }
        
        public int ColumnSpan { get; set; }

        public bool IsBold { get; set; }

        public Color BackgroundColor { get; set; }

        public List<Border> Borders { get; set; }
    }
}