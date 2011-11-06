namespace GiveCRM.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using GiveCRM.BusinessLogic;
    using GiveCRM.Models;

    public class DonationController : Controller
    {
        private readonly IDonationsService donationsService;

        public DonationController(IDonationsService donationsService)
        {
            this.donationsService = donationsService;
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TopDonations()
        {
            var donations = this.donationsService.GetTopDonations();

            return View("DonationList", donations);
        }

        [HttpGet]
        public ActionResult LatestDonations()
        {
            var donations = this.donationsService.GetLatestDonations();

            return View("DonationList", donations);
        }

        [HttpGet]
        public ActionResult QuickDonate(int id)
        {
            return View(id);
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
