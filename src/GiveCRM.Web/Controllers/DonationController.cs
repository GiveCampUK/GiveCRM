using System.Linq;
using GiveCRM.Web.Models.Donation;

namespace GiveCRM.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using GiveCRM.BusinessLogic;
    using GiveCRM.Models;

    public class DonationController : Controller
    {
        private readonly IDonationsService donationsService;
        private readonly ICampaignService campaignService;

        public DonationController(IDonationsService donationsService, ICampaignService campaignService)
        {
            if (donationsService == null)
            {
                throw new ArgumentNullException("donationsService");
            }

            if (campaignService == null)
            {
                throw new ArgumentNullException("campaignService");
            }
            
            this.donationsService = donationsService;
            this.campaignService = campaignService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TopDonations()
        {
            var donations = this.donationsService.GetTopDonations(5);

            return View("DonationList", donations);
        }

        [HttpGet]
        public ActionResult LatestDonations()
        {
            var donations = this.donationsService.GetLatestDonations(5);

            return View("DonationList", donations);
        }

        [HttpGet]
        public ActionResult QuickDonate(int id)
        {
            var campaigns = this.campaignService.GetAllOpen();
            var viewModel = new QuickDonateViewModel(id, campaigns);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult DoQuickDonate(int id, int amount, string date, int campaignId)
        {
            this.donationsService.QuickDonation(new Donation
                                                {
                                                    Amount = amount,
                                                    Date = DateTime.Parse(date),
                                                    MemberId = id,
                                                    CampaignId = campaignId
                                                });
            return View();
        }
    }
}
