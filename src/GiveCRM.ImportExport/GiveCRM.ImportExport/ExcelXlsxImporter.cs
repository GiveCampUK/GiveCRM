using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OfficeOpenXml;
using Cell = GiveCRM.ImportExport.Cells.Cell;

namespace GiveCRM.ImportExport
{
    public class ExcelXlsxImporter : IExcelImporter
    {
        internal ExcelWorkbook Workbook;
        internal ExcelPackage Package;

        public void Open(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            Package = new ExcelPackage(stream);
            Workbook = Package.Workbook;
        }

        public IEnumerable<IEnumerable<Cell>> GetRows(int sheetIndex)
        {
            throw new NotImplementedException();
        }
    }
}
