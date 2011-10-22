using System.Web.Mvc;

namespace GiveCRM.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to GiveCMS!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
