using System;
using System.Collections.Generic;
using System.IO;
using GiveCRM.Models;

namespace GiveCRM.Web.Services
{
    public class MailingListService : IMailingListService
    {
        public void WriteToStream(IEnumerable<Member> members, Stream stream, OutputFormat outputFormat)
        {
            throw new NotImplementedException();
        }
    }
}