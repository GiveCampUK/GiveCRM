namespace GiveCRM.Web.Models
{
    public class ViewModelBase
    {
        public string Title { get; set; }

        public ViewModelBase(string title)
        {
            Title = title;
        }
    }
}