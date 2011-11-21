namespace GiveCRM.Web.Areas.Admin.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    internal class ElmahResult : ActionResult
    {
        private readonly string resourceType;

        public ElmahResult(): this(null)
        { }

        public ElmahResult(string resourceType)
        {
            this.resourceType = resourceType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var factory = new Elmah.ErrorLogPageFactory();

            if (!string.IsNullOrEmpty(this.resourceType))
            {
                var pathInfo = "/" + this.resourceType;
                context.HttpContext.RewritePath(FilePath(context), pathInfo, context.HttpContext.Request.QueryString.ToString());
            }

            var currentContext = GetCurrentContext(context);

            var httpHandler = factory.GetHandler(currentContext, null, null, null);
            var httpAsyncHandler = httpHandler as IHttpAsyncHandler;

            if (httpAsyncHandler != null)
            {
                httpAsyncHandler.BeginProcessRequest(currentContext, r => { }, null);
                return;
            }

            httpHandler.ProcessRequest(currentContext);
        }

        private static HttpContext GetCurrentContext(ControllerContext context)
        {
            var currentApplication = (HttpApplication)context.HttpContext.GetService(typeof(HttpApplication));
            return currentApplication.Context;
        }

        private string FilePath(ControllerContext context)
        {
            return this.resourceType != "stylesheet" ?
                context.HttpContext.Request.Path.Replace(String.Format("/{0}", this.resourceType), string.Empty) : 
                context.HttpContext.Request.Path;
        }
    }
}