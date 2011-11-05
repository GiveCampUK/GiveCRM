
namespace GiveCRM.Web.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to GiveCMS!";

            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }
    }
}
