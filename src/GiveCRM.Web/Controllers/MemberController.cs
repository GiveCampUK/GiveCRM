namespace GiveCRM.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;
    using GiveCRM.Models;
    using GiveCRM.Web.Models.Members;
    using GiveCRM.Web.Services;
    using PagedList;

    public class MemberController : Controller
    {
        private IDonationsService donationsService;
        private IMemberService memberService;
        private ICampaignService campaignService;
        private const int DefaultPageSize = 25;

        public MemberController(IDonationsService donationsService, IMemberService memberService, ICampaignService campaignService)
        {
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

            ViewBag.Campaigns = this.campaignService.AllOpen().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

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

        [HttpGet]
        public ActionResult Search(MemberSearchViewModel model)
        {
            if (string.IsNullOrEmpty(model.SearchButton) && !model.Page.HasValue)
            {
                model.Results = CreatePagedListOfMembers(this.memberService.All(), model);
                return View(model);
            }

            var searchResults = this.memberService.Search(model.Name, model.PostCode, model.Reference);
            model.Results = CreatePagedListOfMembers(searchResults, model);

            return View(model);
        }

        private PagedMemberListViewModel CreatePagedListOfMembers(IEnumerable<Member> memberList, MemberSearchViewModel viewModel)
        {
            return new PagedMemberListViewModel(memberList.ToPagedList(pageNumber: viewModel.Page ?? 1, pageSize: DefaultPageSize), 
                                                page => Url.Action("Index", new RouteValueDictionary
                                                                                {
                                                                                    {"Page", page},
                                                                                    {"Name", viewModel.Name},
                                                                                    {"PostCode", viewModel.PostCode},
                                                                                    {"Reference", viewModel.Reference}
                                                                                }));
        }

        [HttpGet]
        public ActionResult AjaxSearch(string criteria)
        {
            var results = this.memberService.Search(criteria);

            return View(results.Take(DefaultPageSize));
        }

        [HttpGet]
        public ActionResult TopDonors()
        {
            var members = this.memberService.All().OrderByDescending(m => m.TotalDonations).Take(5);

            return View("TopDonors", members);
        }
    }
}
