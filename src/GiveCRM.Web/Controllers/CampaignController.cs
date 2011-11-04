using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GiveCRM.DataAccess;
using GiveCRM.Models;
using GiveCRM.Models.Search;
using GiveCRM.Web.Infrastructure;
using GiveCRM.Web.Models.Campaigns;
using GiveCRM.Web.Models.Search;
using GiveCRM.Web.Properties;
using GiveCRM.Web.Services;

namespace GiveCRM.Web.Controllers
{
    public class CampaignController : Controller
    {
        private readonly IMailingListService _mailingListService;
        private readonly ISearchService _searchService;
        private readonly ICampaignService _campaignService;
        private readonly IMemberSearchFilterService _memberSearchFilterService;
        private readonly IMemberService _memberService;
        private readonly CampaignRuns campaignRuns = new CampaignRuns();
        
        public CampaignController(IMailingListService mailingListService, ISearchService searchService, 
            ICampaignService campaignService, IMemberSearchFilterService memberSearchFilterService,
            IMemberService memberService)
        {
            _mailingListService = mailingListService;
            _searchService = searchService;
            _campaignService = campaignService;
            _memberSearchFilterService = memberSearchFilterService;
            _memberService = memberService;
        }

        public ActionResult Index(bool showClosed = false)
        {
            IEnumerable<Campaign> campaigns = showClosed ? _campaignService.AllClosed() : _campaignService.AllOpen();

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
        public ActionResult Create(Campaign campaign)
        {
            var newId = InsertCampaign(campaign);
            return RedirectToAction("Show", new { id = newId });
        }

        private int InsertCampaign(Campaign campaign)
        {
            var savedCampaign = _campaignService.Insert(campaign);
            return savedCampaign.Id;
        }

        [HttpGet]
        public ActionResult Show(int id)
        {
            var campaign = _campaignService.Get(id);

            var applicableMembers = _searchService.RunCampaign(id);

            var model = new CampaignShowViewModel(Resources.Literal_ShowCampaign)
                            {
                                Campaign = campaign,
                                SearchFilters = _memberSearchFilterService.ForCampaign(id).Select(
                                m =>
                                    new MemberSearchFilterViewModel
                                        {
                                            MemberSearchFilterId = m.Id,
                                            CampaignId = campaign.Id,
                                            CriteriaDisplayText = SearchCriteria.Create(m.InternalName,
                                                                            m.DisplayName,
                                                                            (SearchFieldType)m.FilterType,
                                                                            (SearchOperator)m.SearchOperator,
                                                                            m.Value
                                                                      ).ToFriendlyDisplayString()
                                        }).ToList(),
                                NoSearchFiltersText = Resources.Literal_NoSearchFiltersText,
                                NoMatchingMembersText = Resources.Literal_NoMatchingMembersText,
                                ApplicableMembers = applicableMembers.ToList()
                            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Show(CampaignShowViewModel campaignViewModel)
        {
            var campaign = campaignViewModel.Campaign;
            _campaignService.Update(campaign);
            return RedirectToAction("Show", new { id = campaign.Id });
        }

        [HttpGet]
        public ActionResult AddMembershipSearchFilter(int campaignId)
        {
            var emptySearchCriteria = _searchService.GetEmptySearchCriteria();
            var criteriaNames = emptySearchCriteria.Select(c => c.DisplayName);
            var searchOperators = ((SearchOperator[])Enum.GetValues(typeof(SearchOperator)));

            var model = new AddSearchFilterViewModel(Resources.Literal_AddSearchFilter)
                            {
                                CampaignId = campaignId,
                                CriteriaNames = criteriaNames.Select(s => new SelectListItem { Value = s, Text = s }),
                                SearchOperators = searchOperators.Select(o => new SelectListItem { Value = o.ToString(), Text = o.ToFriendlyDisplayString() })
                            };
            return View("AddSearchFilter", model);
        }

        [HttpPost]
        public ActionResult AddMembershipSearchFilter(AddSearchFilterViewModel viewModel)
        {
            var searchCriteria = _searchService.GetEmptySearchCriteria();

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
            _memberSearchFilterService.Insert(memberSearchFilter);
            return RedirectToAction("Show", new { id = viewModel.CampaignId });
        }

        [HttpGet]
        public ActionResult DeleteMemberSearchFilter(int campaignId, int memberSearchFilterId)
        {
            _memberSearchFilterService.Delete(memberSearchFilterId);
            return RedirectToAction("Show", new { id = campaignId });
        }

        [HttpGet]
        public ActionResult CloseCampaign(int campaignId)
        {
            var campaign = _campaignService.Get(campaignId);
            var viewModel = new SimpleCampaignViewModel(Resources.Literal_CloseCampaign)
                                {
                                    CampaignId = campaignId,
                                    CampaignName = campaign.Name
                                };
            return View("Close", viewModel);
        }

        [HttpPost]
        public ActionResult CloseCampaign(SimpleCampaignViewModel viewModel)
        {
            var campaign = _campaignService.Get(viewModel.CampaignId);
            campaign.IsClosed = "Y";
            _campaignService.Update(campaign);
            return RedirectToAction("Index", new { showClosed = true });
        }

        [HttpGet]
        public ActionResult CommitCampaign(int campaignId)
        {
            var campaign = _campaignService.Get(campaignId);
            var viewModel = new SimpleCampaignViewModel(Resources.Literal_CommitCampaign)
                                {
                                    CampaignId = campaignId,
                                    CampaignName = campaign.Name
                                };
            return View("Commit", viewModel);
        }

        [HttpPost]
        public ActionResult CommitCampaign(SimpleCampaignViewModel viewModel)
        {
            campaignRuns.Commit(viewModel.CampaignId);
            return RedirectToAction("Show", new { id = viewModel.CampaignId });
        }

        public FileResult DownloadMailingList(int id)
        {
            var members = _memberService.FromCampaignRun(id);

            byte[] filecontent;
            using (var stream = new MemoryStream())
            {
                _mailingListService.WriteToStream(members, stream, OutputFormat.XLS);
                filecontent = stream.ToArray();
            }

            return File(filecontent, "application/vnd.ms-excel");
        }

        [HttpGet]
        public ActionResult Clone(int id)
        {
            var campaign = _campaignService.Get(id);
            var campaignClone = new Campaign
                                         {
                                             Id = 0,
                                             Description = campaign.Description,
                                             IsClosed = "N",
                                             Name = campaign.Name + " (" + Resources.Literal_Cloned + ")",
                                             RunOn = null
                                         };

            campaignClone = _campaignService.Insert(campaignClone);

            IEnumerable<MemberSearchFilter> memberSearchFilters = _memberSearchFilterService.ForCampaign(id);

            foreach (MemberSearchFilter memberSearchFilter in memberSearchFilters)
            {
                _memberSearchFilterService.Insert(new MemberSearchFilter
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
