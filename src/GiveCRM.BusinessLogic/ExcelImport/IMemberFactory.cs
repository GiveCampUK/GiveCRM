using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.BusinessLogic.ExcelImport
{
    internal interface IMemberFactory
    {
        Member CreateMember(IDictionary<string, object> memberData);
    }
}