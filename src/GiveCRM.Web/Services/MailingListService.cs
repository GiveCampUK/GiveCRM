using System;
using System.Collections.Generic;
using System.IO;
using GiveCRM.ImportExport;
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
            throw new NotImplementedException();

            // Build excel graph of cells
            // Ask excel exporter to write it to the stream
        }
    }
}