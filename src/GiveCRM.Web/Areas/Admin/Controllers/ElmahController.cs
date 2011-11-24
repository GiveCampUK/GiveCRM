namespace GiveCRM.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    //[Authorize(Roles = "Admin")]
    public class ElmahController : Controller
    {
        public ActionResult Index()
        {
            return new ElmahResult();
        }

        public ActionResult Stylesheet()
        {
            return new ElmahResult("stylesheet");
        }

        public ActionResult Rss()
        {
            return new ElmahResult("rss");
        }

        public ActionResult DigestRss()
        {
            return new ElmahResult("digestrss");
        }

        public ActionResult About()
        {
            return new ElmahResult("about");
        }

        public ActionResult Detail()
        {
            return new ElmahResult("detail");
        }

        public ActionResult Download()
        {
            return new ElmahResult("download");
        }

        public ActionResult Json()
        {
            return new ElmahResult("json");
        }

        public ActionResult Xml()
        {
            return new ElmahResult("xml");
        }
    }
}
