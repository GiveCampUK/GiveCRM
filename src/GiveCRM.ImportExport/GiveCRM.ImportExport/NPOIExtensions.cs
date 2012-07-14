namespace GiveCRM.ImportExport
{
    using System.Collections.Generic;
    using System.Drawing;
    using NPOI.SS.UserModel;
    using BorderStyle = GiveCRM.ImportExport.Borders.BorderStyle;

    public static class NPOIExtensions
    {

        public static short ToNPOIColor(this Color val)
        {
            short npoiColor;
            if (!colorList.TryGetValue(val, out npoiColor))
            {
                npoiColor = IndexedColors.WHITE.Index;
            }

            return npoiColor;
        }

        public static CellBorderType NPOIBorderType(this BorderStyle val)
        {
            CellBorderType npoiType;
            if(!borderStyleMap.TryGetValue(val, out npoiType))
            {
                npoiType = CellBorderType.NONE;
            }

            return npoiType;
        }

        private static readonly Dictionary<BorderStyle, CellBorderType> borderStyleMap = new Dictionary<BorderStyle, CellBorderType>
        {
                {BorderStyle.DashDot,CellBorderType.DASH_DOT},
                {BorderStyle.DashDotDot,CellBorderType.DASH_DOT_DOT },
                {BorderStyle.Dashed,CellBorderType.DASHED },
                {BorderStyle.Dotted,CellBorderType.DOTTED },
                {BorderStyle.Double,CellBorderType.DOUBLE },
                {BorderStyle.Hair,CellBorderType.HAIR },
                {BorderStyle.Medium,CellBorderType.MEDIUM },
                {BorderStyle.MediumDashDot,CellBorderType.MEDIUM_DASH_DOT },
                {BorderStyle.MediumDashDotDot,CellBorderType.MEDIUM_DASH_DOT_DOT },
                {BorderStyle.MediumDashed,CellBorderType.MEDIUM_DASHED },
                {BorderStyle.None,CellBorderType.NONE },
                {BorderStyle.SlantedDashDot,CellBorderType.SLANTED_DASH_DOT },
                {BorderStyle.Thick,CellBorderType.THICK },
                {BorderStyle.Thin,CellBorderType.THIN }                                                                   
        };

        private static Dictionary<Color, short> colorList = new Dictionary<Color, short>
        {
            {Color.SlateBlue, IndexedColors.BLUE_GREY.Index},
            {Color.LightGray, IndexedColors.GREY_25_PERCENT.Index},
            {Color.Gray, IndexedColors.GREY_40_PERCENT.Index},
            {Color.DimGray, IndexedColors.GREY_50_PERCENT.Index},
            {Color.DarkGray, IndexedColors.GREY_80_PERCENT.Index},
            {Color.Red, IndexedColors.RED.Index}
        };
    }
}

