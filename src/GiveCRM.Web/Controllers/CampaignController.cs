using System.Collections.Generic;
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

            string title, linkText;

            if (showClosed)
            {
                title = Resources.Literal_Closed;
                linkText = Resources.Literal_Open;
            }
            else
            {
                title = Resources.Literal_Open;
                linkText = Resources.Literal_Closed;
            }

            title = string.Format("{0} {1}", title, Resources.Literal_Campaigns);
            linkText = string.Format(Resources.Show_Campaigns_Text, linkText);

            var model = new CampaignIndexViewModel(title)
                            {
                                ShowCampaignsLinkText = linkText,
                                CreateCampaignLinkText = Resources.Literal_CreateCampaign,
                                ShowClosed = showClosed,
                                Campaigns = openCampaigns
                            };

            return View(model);
        }

        public ActionResult Create()
        {
            var model = new CampaignShowViewModel(Resources.Literal_CreateCampaign);
            return View("Show", model);
        }
    }
}
