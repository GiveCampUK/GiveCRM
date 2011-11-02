using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GiveCRM.Models;
using GiveCRM.Web.Infrastructure;
using GiveCRM.Web.Models.Members;
using GiveCRM.Web.Services;

namespace GiveCRM.Web.Controllers
{
    public class MemberController : Controller
    {
        private IDonationsService _donationsService;
        private IMemberService _memberService;
        private ICampaignService _campaignService;
        private const int DefaultPageSize = 25;

        public MemberController(IDonationsService donationsService, IMemberService memberService, ICampaignService campaignService)
        {
            _donationsService = donationsService;
            _memberService = memberService;
            _campaignService = campaignService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            ViewBag.Title = "Add Member"; 
            return View(new MemberEditViewModel() { PhoneNumbers = new List<PhoneNumber>() });
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Edit Member";
            var model = _memberService.Get(id);
            if(model.PhoneNumbers == null) 
               model.PhoneNumbers = new List<PhoneNumber>(); 
            return View(viewName: "Add", model: MemberEditViewModel.ToViewModel(model));
        }

        public ActionResult Delete(int id)
        {
            var member = _memberService.Get(id);

            _memberService.Delete(member); 

            return RedirectToAction("Index");
        }

        public ActionResult Donate(int id)
        {
            ViewBag.MemberName = _memberService.Get(id).ToString();

            ViewBag.Campaigns = _campaignService.AllOpen().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

            return View(new Donation { MemberId = id });
        }

        public ActionResult SaveDonation(Donation donation)
        {
            _donationsService.QuickDonation(donation);

            return RedirectToAction("Index");
        }

        public ActionResult Save(MemberEditViewModel member)
        {
            ViewBag.Title = member.Id == 0 ? "Add Member" : "Edit Member";

            if(!ModelState.IsValid)
                return View(viewName: "Add", model: member);

            _memberService.Save(member.ToModel()); 

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(string name, string postcode, string reference, int page = 1, int pageSize = DefaultPageSize)
        {
            var results = _memberService.Search(name, postcode, reference);

            return View(new PagedList<Member>(results, page - 1, pageSize));
        }

        [HttpGet]
        public ActionResult AjaxSearch(string criteria)
        {
            var results = _memberService.Search(criteria);

            return View(results.Take(DefaultPageSize));
        }

        public ActionResult TopDonors()
        {
            var members = _memberService.All().OrderByDescending(m => m.TotalDonations).Take(5);

            return View("MembersList", members);
        }
    }
}
