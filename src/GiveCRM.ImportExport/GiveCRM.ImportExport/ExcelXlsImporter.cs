using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GiveCRM.ImportExport.Cells;
using NPOI.HSSF.UserModel;

namespace GiveCRM.ImportExport
{
    public class ExcelXlsImporter : IExcelImporter
    {
        internal HSSFWorkbook Workbook;

        public void Open(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            Workbook = new HSSFWorkbook(stream);
        }

        public IEnumerable<IEnumerable<Cell>> GetRows(int sheetIndex)
        {
            throw new NotImplementedException();
        }
    }
}
