namespace GiveCRM.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;

    using GiveCRM.BusinessLogic;
    using GiveCRM.Models;
    using GiveCRM.Models.Search;
    using GiveCRM.Web.Infrastructure;
    using GiveCRM.Web.Models.Campaigns;
    using GiveCRM.Web.Models.Search;
    using GiveCRM.Web.Properties;
    
    public class CampaignController : Controller
    {
        private readonly IMailingListService mailingListService;
        private readonly ISearchService searchService;
        private readonly ICampaignService campaignService;
        private readonly IMemberSearchFilterService memberSearchFilterService;
        private readonly IMemberService memberService;
        
        public CampaignController(
            IMailingListService mailingListService, 
            ISearchService searchService, 
            ICampaignService campaignService, 
            IMemberSearchFilterService memberSearchFilterService,
            IMemberService memberService)
        {
            this.mailingListService = mailingListService;
            this.searchService = searchService;
            this.campaignService = campaignService;
            this.memberSearchFilterService = memberSearchFilterService;
            this.memberService = memberService;
        }

        [HttpGet]
        public ActionResult Index(bool showClosed = false)
        {
            IEnumerable<Campaign> campaigns = showClosed ? this.campaignService.GetAllClosed() : this.campaignService.GetAllOpen();
            
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
                                Campaigns = campaigns
                            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CampaignShowViewModel(Resources.Literal_CreateCampaign)
                            {
                                Campaign = new Campaign
                                               {
                                                   Name = "New Campaign"
                                               }
                            };
            return View("Show", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Campaign campaign)
        {
            var newId = this.InsertCampaign(campaign);
            return RedirectToAction("Show", new { id = newId });
        }

        private int InsertCampaign(Campaign campaign)
        {
            var savedCampaign = this.campaignService.Insert(campaign);
            return savedCampaign.Id;
        }

        [HttpGet]
        public ActionResult Show(int id)
        {
            var campaign = this.campaignService.Get(id);
            var applicableMembers = this.memberService.SearchByCampaignId(id).ToList();
            var searchFilters = this.memberSearchFilterService.ForCampaign(id)
                                    .Select(m =>
                                        {
                                            var criteriaDisplayText = SearchCriteria.Create(m.InternalName,
                                                                                            m.DisplayName,
                                                                                            (SearchFieldType) m.FilterType,
                                                                                            (SearchOperator) m.SearchOperator,
                                                                                            m.Value
                                                        ).ToFriendlyDisplayString();
                                            return new MemberSearchFilterViewModel
                                                       {
                                                                   MemberSearchFilterId = m.Id,
                                                                   CampaignId = campaign.Id,
                                                                   CriteriaDisplayText = criteriaDisplayText
                                                       };
                                        }).ToList();

            var model = new CampaignShowViewModel(Resources.Literal_ShowCampaign)
                            {
                                Campaign = campaign,
                                SearchFilters = searchFilters,
                                NoSearchFiltersText = Resources.Literal_NoSearchFiltersText,
                                NoMatchingMembersText = Resources.Literal_NoMatchingMembersText,
                                ApplicableMembers = applicableMembers,
                                IsReadonly = campaign.IsReadonly
                            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Show(CampaignShowViewModel campaignViewModel)
        {
            var campaign = campaignViewModel.Campaign;
            this.campaignService.Update(campaign);
            return RedirectToAction("Show", new { id = campaign.Id });
        }

        [HttpGet]
        public ActionResult AddMembershipSearchFilter(int campaignId)
        {
            var emptySearchCriteria = this.searchService.GetEmptySearchCriteria();
            var criteriaNames = emptySearchCriteria.Select(c => c.DisplayName);
            var searchOperators = (SearchOperator[])Enum.GetValues(typeof(SearchOperator));

            var model = new AddSearchFilterViewModel(Resources.Literal_AddSearchFilter)
                            {
                                CampaignId = campaignId,
                                CriteriaNames = criteriaNames.Select(s => new SelectListItem { Value = s, Text = s }),
                                SearchOperators = searchOperators.Select(o => new SelectListItem { Value = o.ToString(), Text = o.ToFriendlyDisplayString() })
                            };
            return View("AddSearchFilter", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMembershipSearchFilter(AddSearchFilterViewModel viewModel)
        {
            var searchCriteria = this.searchService.GetEmptySearchCriteria();

            // we have to find one
            var searchCriterion = searchCriteria.First(c => c.DisplayName == viewModel.CriteriaName);

            var memberSearchFilter = new MemberSearchFilter
                                             {
                                                 CampaignId = viewModel.CampaignId,
                                                 InternalName = searchCriterion.InternalName,
                                                 FilterType = (int)searchCriterion.Type,
                                                 DisplayName = viewModel.CriteriaName,
                                                 SearchOperator = (int)viewModel.SearchOperator,
                                                 Value = viewModel.Value
                                             };
            this.memberSearchFilterService.Insert(memberSearchFilter);
            return RedirectToAction("Show", new { id = viewModel.CampaignId });
        }

        [HttpGet]
        public ActionResult DeleteMemberSearchFilter(int campaignId, int memberSearchFilterId)
        {
            this.memberSearchFilterService.Delete(memberSearchFilterId);
            return RedirectToAction("Show", new { id = campaignId });
        }

        [HttpGet]
        public ActionResult CloseCampaign(int campaignId)
        {
            var campaign = this.campaignService.Get(campaignId);
            var viewModel = new SimpleCampaignViewModel(Resources.Literal_CloseCampaign)
                                {
                                    CampaignId = campaignId,
                                    CampaignName = campaign.Name
                                };
            return View("Close", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CloseCampaign(SimpleCampaignViewModel viewModel)
        {
            var campaign = this.campaignService.Get(viewModel.CampaignId);
            campaign.IsClosed = "Y";
            this.campaignService.Update(campaign);
            return RedirectToAction("Index", new { showClosed = true });
        }

        [HttpGet]
        public ActionResult CommitCampaign(int campaignId)
        {
            var campaign = this.campaignService.Get(campaignId);
            var viewModel = new SimpleCampaignViewModel(Resources.Literal_CommitCampaign)
                                {
                                    CampaignId = campaignId,
                                    CampaignName = campaign.Name
                                };
            return View("Commit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CommitCampaign(SimpleCampaignViewModel viewModel)
        {
            this.campaignService.Commit(viewModel.CampaignId);
            return RedirectToAction("Show", new { id = viewModel.CampaignId });
        }

        [HttpGet]
        public FileResult DownloadMailingList(int id)
        {
            var members = this.memberService.FromCampaignRun(id);

            byte[] filecontent;
            using (var stream = new MemoryStream())
            {
                this.mailingListService.WriteToStream(members, stream, OutputFormat.XLS);
                filecontent = stream.ToArray();
            }

            return File(filecontent, "application/vnd.ms-excel");
        }

        [HttpGet]
        public ActionResult Clone(int id)
        {
            var campaign = this.campaignService.Get(id);
            var campaignClone = new Campaign
                                         {
                                             Id = 0,
                                             Description = campaign.Description,
                                             IsClosed = "N",
                                             Name = campaign.Name + " (" + Resources.Literal_Cloned + ")",
                                             RunOn = null
                                         };

            campaignClone = this.campaignService.Insert(campaignClone);

            IEnumerable<MemberSearchFilter> memberSearchFilters = this.memberSearchFilterService.ForCampaign(id);

            foreach (MemberSearchFilter memberSearchFilter in memberSearchFilters)
            {
                this.memberSearchFilterService.Insert(new MemberSearchFilter
                                                  {
                                                      CampaignId = campaignClone.Id,
                                                      DisplayName = memberSearchFilter.DisplayName,
                                                      FilterType = memberSearchFilter.FilterType,
                                                      InternalName = memberSearchFilter.InternalName,
                                                      SearchOperator = memberSearchFilter.SearchOperator,
                                                      Value = memberSearchFilter.Value
                                                  });
            }

            return RedirectToAction("Show", new { id = campaignClone.Id });
        }
    }
}
