using System.Drawing;

namespace GiveCRM.ImportExport.Borders
{
    public class Border
    {
        public Border()
        {
            Location = BorderLocation.None;
            Style = BorderStyle.None;
            this.Color = System.Drawing.Color.Black;
        }

        public BorderLocation Location { get; set; }

        public BorderStyle Style { get; set; }

        public Color Color { get; set; }
    }
}