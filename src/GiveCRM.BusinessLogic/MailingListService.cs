using System.Collections.Generic;
using System.IO;
using System.Linq;
using GiveCRM.ImportExport;
using GiveCRM.ImportExport.Cells;
using GiveCRM.Models;

namespace GiveCRM.BusinessLogic
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
            var rows = GetHeaderRows().Concat(GetDataRows(members));
            _excelExport.WriteDataToExport(rows, new CellFormatter(), ExcelFileType.XLS);
            _excelExport.ExportToStream(stream);
        }

        private List<List<Cell>> GetDataRows(IEnumerable<Member> members)
        {
            return members.Select(m => new List<Cell>
                                           {
                                               new Cell {Value = m.Reference},
                                               new Cell {Value = m.Title},
                                               new Cell {Value= m.Salutation},
                                               new Cell {Value= m.FirstName},
                                               new Cell {Value= m.LastName},
                                               new Cell {Value= m.EmailAddress},
                                               new Cell {Value= m.AddressLine1},
                                               new Cell {Value= m.AddressLine2},
                                               new Cell {Value= m.City},
                                               new Cell {Value= m.Region},
                                               new Cell {Value= m.PostalCode},
                                               new Cell {Value= m.Country},
                                               new Cell {Value= FirstTelephoneNumber(m, PhoneNumberType.Home)},
                                               new Cell {Value= FirstTelephoneNumber(m, PhoneNumberType.Mobile)},
                                               new Cell {Value= FirstTelephoneNumber(m, PhoneNumberType.Work)},
                                           }).ToList();
        }

        private string FirstTelephoneNumber(Member member, PhoneNumberType phoneNumberType)
        {
            PhoneNumber phoneNumber = member.PhoneNumbers.FirstOrDefault(number => number.PhoneNumberType == phoneNumberType);
            return phoneNumber != null ? phoneNumber.Number : "";
        }

        private List<List<Cell>> GetHeaderRows()
        {
            var ret = new List<List<Cell>>
                          {
                              new List<Cell>
                                  {
                                      new Cell {Value = "Reference", IsBold = true},
                                      new Cell {Value = "Title", IsBold = true},
                                      new Cell {Value = "Salutation", IsBold = true},
                                      new Cell {Value = "Firstname", IsBold = true},
                                      new Cell {Value = "Surname", IsBold = true},
                                      new Cell {Value = "Email", IsBold = true},
                                      new Cell {Value = "Address 1", IsBold = true},
                                      new Cell {Value = "Address 2", IsBold = true},
                                      new Cell {Value = "City", IsBold = true},
                                      new Cell {Value = "Region", IsBold = true},
                                      new Cell {Value = "Postal Code", IsBold = true},
                                      new Cell {Value = "Country", IsBold = true},
                                      new Cell {Value = "Home Telephone", IsBold = true},
                                      new Cell {Value = "Mobile Telephone", IsBold = true},
                                      new Cell {Value = "Work Telephone", IsBold = true},
                                  }
                          };
            return ret;
        }
    }
}