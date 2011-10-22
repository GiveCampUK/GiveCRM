using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GiveCRM.ImportExport.Cells;

namespace GiveCRM.ImportExport
{
    public interface IExcelImporter
    {
        void Open(Stream stream);
        IEnumerable<IEnumerable<Cell>> GetRows(int sheetIndex);
    }
}
