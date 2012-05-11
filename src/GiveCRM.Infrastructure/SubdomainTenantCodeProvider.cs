namespace GiveCRM.Infrastructure
{
    using System.Web;

    public class SubdomainTenantCodeProvider : ITenantCodeProvider
    {
        public string GetTenantCode()
        {
            return this.GetTenantCode(HttpContext.Current.Request.Url.Host);
        }

        public string GetTenantCode(string hostname)
        {
            // What if it's an IP address? IPv4? IPv6?
            // What if it's "localhost"?
            // What if there is no subdomain?
            // What if the subdomain is "www"?
            // Should it be compared to an app setting for acceptable values?
            return "";
        }
    }
}