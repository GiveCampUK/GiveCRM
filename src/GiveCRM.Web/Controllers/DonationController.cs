using System;
using System.Web.Mvc;
using GiveCRM.Models;
using GiveCRM.Web.Services;

namespace GiveCRM.Web.Controllers
{
    public class DonationController : Controller
    {
        private readonly IDonationsService _donationsService;

        public DonationController(IDonationsService donationsService)
        {
            _donationsService = donationsService;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TopDonations()
        {
            var donations = _donationsService.GetTopDonations();

            return View("DonationList", donations);
        }

        public ActionResult LatestDonations()
        {
            var donations = _donationsService.GetLatestDonations();

            return View("DonationList", donations);
        }

        public ActionResult QuickDonate(int id)
        {
            return View(id);
        }

        public ActionResult DoQuickDonate(int id, int amount, string date, int campaignId)
        {
            _donationsService.QuickDonation(new Donation
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
