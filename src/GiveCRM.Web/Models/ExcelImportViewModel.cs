namespace GiveCRM.Web.Models
{
    using System.Web;

    public class ExcelImportViewModel : ViewModelBase
    {
        public readonly string ExcelTemplatePath = "~/Content/files/GiveCRM_Template.xls";

        public HttpPostedFileBase File { get; set; }

        public ExcelImportViewModel() : base(string.Empty)
        {
        }

        public ExcelImportViewModel(string title) : base(title)
        {
            
        }
    }
}