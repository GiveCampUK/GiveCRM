namespace GiveCRM.Infrastructure
{
    using System.Web;

    public class DomainNameTenantCodeProvider : ITenantCodeProvider
    {
        private readonly IHttpRequest request;

        public DomainNameTenantCodeProvider(IHttpRequest request)
        {
            this.request = request;
        }

        public string GetTenantCode()
        {
            return request.Url.Host;
        }
    }
}