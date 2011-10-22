using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GiveCRM.DataAccess;
using GiveCRM.Models;
using GiveCRM.Web.Models.Campaign;

namespace GiveCRM.Web.Controllers
{
    public class CampaignController : Controller
    {
        //
        // GET: /Campaign/

        public ActionResult Index()
        {
            var campaigns = new Campaigns();
            IEnumerable<Campaign> openCampaigns = campaigns.AllOpen();

            var model = new CampaignIndexViewModel { Campaigns = openCampaigns };

            return View(model);
        }

    }
}
