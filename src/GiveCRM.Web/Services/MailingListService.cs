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
            var rows = GetHeaderRows().Concat(GetDataRows(members));
            _excelExport.WriteDataToExport(rows, new CellFormatter(), ExcelFileType.XLS);
            _excelExport.ExportToStream(stream);
        }

        private List<List<Cell>> GetDataRows(IEnumerable<Member> members)
        {
            return members.Select(m => new List<Cell>
                                           {
                                               new Cell {Value = m.Reference, BackgroundColor = Color.Empty},
                                               new Cell {Value = m.Title, BackgroundColor = Color.Empty},
                                               new Cell {Value= m.Salutation, BackgroundColor = Color.Empty},
                                               new Cell {Value= m.FirstName, BackgroundColor = Color.Empty},
                                               new Cell {Value= m.LastName, BackgroundColor = Color.Empty},
                                               new Cell {Value= m.EmailAddress, BackgroundColor = Color.Empty},
                                               new Cell {Value= m.AddressLine1, BackgroundColor = Color.Empty},
                                               new Cell {Value= m.AddressLine2, BackgroundColor = Color.Empty},
                                               new Cell {Value= m.City, BackgroundColor = Color.Empty},
                                               new Cell {Value= m.Region, BackgroundColor = Color.Empty},
                                               new Cell {Value= m.PostalCode, BackgroundColor = Color.Empty},
                                               new Cell {Value= m.Country, BackgroundColor = Color.Empty},
                                               new Cell {Value= FirstTelephoneNumber(m, PhoneNumberType.Home), BackgroundColor = Color.Empty},
                                               new Cell {Value= FirstTelephoneNumber(m, PhoneNumberType.Mobile), BackgroundColor = Color.Empty},
                                               new Cell {Value= FirstTelephoneNumber(m, PhoneNumberType.Work), BackgroundColor = Color.Empty},
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
                                      new Cell {Value = "Reference", IsBold = true, BackgroundColor = Color.Empty},
                                      new Cell {Value = "Title", IsBold = true, BackgroundColor = Color.Empty},
                                      new Cell {Value = "Salutation", IsBold = true, BackgroundColor = Color.Empty},
                                      new Cell {Value = "Firstname", IsBold = true, BackgroundColor = Color.Empty},
                                      new Cell {Value = "Surname", IsBold = true, BackgroundColor = Color.Empty},
                                      new Cell {Value = "Email", IsBold = true, BackgroundColor = Color.Empty},
                                      new Cell {Value = "Address 1", IsBold = true, BackgroundColor = Color.Empty},
                                      new Cell {Value = "Address 2", IsBold = true, BackgroundColor = Color.Empty},
                                      new Cell {Value = "City", IsBold = true, BackgroundColor = Color.Empty},
                                      new Cell {Value = "Region", IsBold = true, BackgroundColor = Color.Empty},
                                      new Cell {Value = "Postal Code", IsBold = true, BackgroundColor = Color.Empty},
                                      new Cell {Value = "Country", IsBold = true, BackgroundColor = Color.Empty},
                                      new Cell {Value = "Home Telephone", IsBold = true, BackgroundColor = Color.Empty},
                                      new Cell {Value = "Mobile Telephone", IsBold = true, BackgroundColor = Color.Empty},
                                      new Cell {Value = "Work Telephone", IsBold = true, BackgroundColor = Color.Empty},
                                  }
                          };
            return ret;
        }
    }
}