using System.Collections.Generic;
using System.IO;
using GiveCRM.Models;

namespace GiveCRM.BusinessLogic
{
    public interface IMailingListService
    {
        void WriteToStream(IEnumerable<Member> members, Stream stream, OutputFormat outputFormat);
    }

    public enum OutputFormat
    {
        XLS,
        XLSX,
        // CSV?,
        // vCard?
    }
}