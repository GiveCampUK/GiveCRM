using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GiveCRM.DataAccess;
using GiveCRM.Models;
using GiveCRM.Web.Models.Campaign;
using GiveCRM.Web.Properties;

namespace GiveCRM.Web.Controllers
{
    public class CampaignController : Controller
    {
        //
        // GET: /Campaign/

        public ActionResult Index(bool showClosed = false)
        {
            var campaigns = new Campaigns();
            IEnumerable<Campaign> openCampaigns = showClosed ? campaigns.AllClosed() : campaigns.AllOpen();

            string title = showClosed ? Resources.Literal_Closed : Resources.Literal_Open;
            title = string.Format("{0} {1}", title, Resources.Literal_Campaigns);

            var model = new CampaignIndexViewModel
                            {
                                Title = title,
                                Campaigns = openCampaigns
                            };

            return View(model);
        }

    }
}
