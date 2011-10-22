using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using GiveCRM.ImportExport;
using GiveCRM.ImportExport.Cells;
using GiveCRM.Models;

namespace GiveCRM.Web.Services
{
    public class MailingListService : IMailingListService
    {
        private readonly IExcelExport _excelExport;

        public MailingListService(IExcelExport excelExport)
        {
            _excelExport = excelExport;
        }

        public void WriteToStream(IEnumerable<Member> members, Stream stream, OutputFormat outputFormat)
        {
            // Build excel graph of cells
            List<List<Cell>> rows = members.Select(CreateRow).ToList();



            // Ask excel exporter to write it to the stream
            _excelExport.ExportToStream(stream);
        }

        private List<Cell> CreateRow(Member member)
        {
            return new[]
                       {
                           new Cell{Value = "Reference", IsBold = true},
                           new Cell{Value = "Title", IsBold = true},
                           new Cell{Value = "Salutation", IsBold = true},
                           new Cell{Value = "Firstname", IsBold = true},
                           new Cell{Value = "Surname", IsBold = true},
                           new Cell{Value = "Email", IsBold = true},
                           new Cell{Value = "Address 1", IsBold = true},
                           new Cell{Value = "Address 2", IsBold = true},
                           new Cell{Value = "City", IsBold = true},
                           new Cell{Value = "Region", IsBold = true},
                           new Cell{Value = "Postal Code", IsBold = true},
                           new Cell{Value = "Country", IsBold = true},
                           new Cell{Value = "Home Telephone", IsBold = true},
                           new Cell{Value = "Mobile Telephone", IsBold = true},
                       }.ToList();
        }
    }
}