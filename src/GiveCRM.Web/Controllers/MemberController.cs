

using System;
using GiveCRM.BusinessLogic;

namespace GiveCRM.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using GiveCRM.Models;
    using GiveCRM.Web.Models.Members;
    using GiveCRM.Web.Services;

    using System.Web.Routing;
    using PagedList;

    public class MemberController : Controller
    {
        private const int MaxResults = 25;

        private readonly IDonationsService donationsService;
        private readonly IMemberService memberService;
        private readonly ICampaignService campaignService;

        public MemberController(IDonationsService donationsService, IMemberService memberService, ICampaignService campaignService)
        {
            if (donationsService == null)
            {
                throw new ArgumentNullException("donationsService");
            }

            if (memberService == null)
            {
                throw new ArgumentNullException("memberService");
            }
            
            if (campaignService == null)
            {
                throw new ArgumentNullException("campaignService");
            }
            
            this.donationsService = donationsService;
            this.memberService = memberService;
            this.campaignService = campaignService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Title = "Add Member"; 
            return View(new MemberEditViewModel() { PhoneNumbers = new List<PhoneNumber>() });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Edit Member";
            var model = this.memberService.Get(id);
            if (model.PhoneNumbers == null)
            {
                model.PhoneNumbers = new List<PhoneNumber>();
            }

            return View(viewName: "Add", model: MemberEditViewModel.ToViewModel(model));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var member = this.memberService.Get(id);

            this.memberService.Delete(member); 

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Donate(int id)
        {
            ViewBag.MemberName = this.memberService.Get(id).ToString();

            ViewBag.Campaigns = this.campaignService.GetAllOpen().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

            return View(new Donation { MemberId = id });
        }

        [HttpGet]
        public ActionResult SaveDonation(Donation donation)
        {
            this.donationsService.QuickDonation(donation);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Save(MemberEditViewModel member)
        {
            ViewBag.Title = member.Id == 0 ? "Add Member" : "Edit Member";

            if (!ModelState.IsValid)
            {
                return View(viewName: "Add", model: member);
            }

            this.memberService.Save(member.ToModel()); 

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string name, string postcode, string reference, int start = 0)
        {
            var results = this.memberService.Search(name, postcode, reference);

            return View(new MemberSearchViewModel { Results = results.Take(MaxResults), AreMore = results.Count() > MaxResults });
        }

        public ActionResult AjaxSearch(string criteria)
        {
            var results = this.memberService.Search(criteria);

            return View(results.Take(MaxResults));
        }

        [HttpGet]
        public ActionResult TopDonors()
        {
            var members = this.memberService.All().OrderByDescending(m => m.TotalDonations).Take(5);

            return View("MembersList", members);
        }
    }
}
