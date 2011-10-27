using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GiveCRM.Models;
using GiveCRM.Web.Models.Members;
using GiveCRM.Web.Services;

namespace GiveCRM.Web.Controllers
{
    public class MemberController : Controller
    {
        private const int MaxResults = 25;

        private IDonationsService _donationsService;
        private IMemberService _memberService;
        private ICampaignService _campaignService;

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

            member.AddressLine1 = "deleted";
            member.AddressLine2 = "deleted";
            member.EmailAddress = "deleted";
            member.FirstName = "deleted";
            member.LastName = "deleted";
            
            member.Archived = true;

            _memberService.Update(member);

            return RedirectToAction("Index");
        }

        public ActionResult Donate(int id)
        {
            ViewBag.MemberName = GetFormattedName(_memberService.Get(id));

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

            if (member.Id == 0)
            {
                _memberService.Insert(member.ToModel());
            }
            else
            {
                _memberService.Update(member.ToModel());
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(string name, string postcode, string reference, int start = 0)
        {
            var results = _memberService.Search(name, postcode, reference);

            return View(new MemberSearchViewModel { Results = results.Take(MaxResults), AreMore = results.Count() > MaxResults });
        }

        [HttpGet]
        public ActionResult AjaxSearch(string criteria)
        {
            var results = _memberService.Search(criteria);

            return View(results.Take(10));
        }

        public ActionResult TopDonors()
        {
            var members = _memberService.All().OrderByDescending(m => m.TotalDonations).Take(5);

            return View("MembersList", members);
        }

        #region helper methods      

        private string GetFormattedName(Member member)
        {
            return string.Format("{0} {1} {2}", member.Salutation, member.FirstName, member.LastName);
        }

        #endregion
    }
}
