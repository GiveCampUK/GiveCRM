namespace GiveCRM.Infrastructure
{
    using System;
    using System.Web;

    public class ThinHttpRequest : IHttpRequest
    {
        private readonly HttpRequest request;

        public ThinHttpRequest(HttpRequest request)
        {
            this.request = request;
        }

        public Uri Url
        {
            get { return this.request.Url; }
        }
    }
}