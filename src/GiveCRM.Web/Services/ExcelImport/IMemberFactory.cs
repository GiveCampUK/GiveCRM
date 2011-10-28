using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.Web.Services.ExcelImport
{
    public interface IMemberFactory
    {
        Member CreateMember(IDictionary<string, object> memberData);
    }
}