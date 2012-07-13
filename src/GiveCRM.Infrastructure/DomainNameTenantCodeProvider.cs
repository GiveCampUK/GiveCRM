namespace GiveCRM.Infrastructure
{
    using System.Configuration;
    using System.Web;

    public class DomainNameTenantCodeProvider : ITenantCodeProvider
    {
        public string GetTenantCode()
        {
            return HttpContext.Current.Request.Url.Host;
        }
    }
}