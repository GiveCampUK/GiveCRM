using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GiveCRM.DataAccess;

namespace GiveCRM.Web.Controllers
{
    public class DonationController : Controller
    {
        private Donations _donationsDb = new Donations();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TopDonations()
        {
            var donations = _donationsDb.All().OrderByDescending(d => d.Amount).Take(5);

            return View("DonationList", donations);
        }

        public ActionResult LatestDonations()
        {
            var donations = _donationsDb.All().OrderByDescending(d => d.Date).Take(5);

            return View("DonationList", donations);
        }

        public ActionResult QuickDonate(int id)
        {
            return View(id);
        }

    }
}
