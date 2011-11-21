namespace GiveCRM.Web.Services
{
    using System.Web.Mvc;

    public interface IUrlValidationService
    {
        bool IsRedirectable(Controller controller,string url);
    }
}