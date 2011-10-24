using System.Web;

namespace GiveCRM.Web.Models
{
    public class ExcelImportViewModel : ViewModelBase
    {
        public readonly string ExcelTemplatePath = "~/Content/files/GiveCRM_Template.xls";
        public HttpPostedFileBase File;

        public ExcelImportViewModel() : base(string.Empty)
        {
        }

        public ExcelImportViewModel(string title) : base(title)
        {
            
        }
    }
}