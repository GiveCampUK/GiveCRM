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
        private readonly Campaigns campaignsRepo = new Campaigns();
        private readonly MemberSearchFilters memberSearchFilterRepo = new MemberSearchFilters();

        public CampaignController(IMailingListService mailingListService, ISearchService searchService)
        {
            _mailingListService = mailingListService;
            _searchService = searchService;
        }

        public ActionResult Index(bool showClosed = false)
        {
            IEnumerable<Campaign> campaigns = showClosed ? campaignsRepo.AllClosed() : campaignsRepo.AllOpen();

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
            var savedCampaign = campaignsRepo.Insert(campaign);
            return savedCampaign.Id;
        }

        [HttpGet]
        public ActionResult Show(int id)
        {
            var campaign = campaignsRepo.Get(id);

            var applicableMembers = _searchService.RunCampaign(id);

            var model = new CampaignShowViewModel(Resources.Literal_ShowCampaign)
                            {
                                Campaign = campaign,
                                SearchFilters = memberSearchFilterRepo.ForCampaign(id).Select(
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
            campaignsRepo.Update(campaign);
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
            memberSearchFilterRepo.Insert(memberSearchFilter);
            return RedirectToAction("Show", new { id = viewModel.CampaignId });
        }

        [HttpGet]
        public ActionResult DeleteMemberSearchFilter(int campaignId, int memberSearchFilterId)
        {
            memberSearchFilterRepo.Delete(memberSearchFilterId);
            return RedirectToAction("Show", new { id = campaignId });
        }

        [HttpGet]
        public ActionResult CloseCampaign(int campaignId)
        {
            var campaign = campaignsRepo.Get(campaignId);
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
            var campaign = campaignsRepo.Get(viewModel.CampaignId);
            campaign.IsClosed = "Y";
            campaignsRepo.Update(campaign);
            return RedirectToAction("Index", new { showClosed = true });
        }

        [HttpGet]
        public ActionResult CommitCampaign(int campaignId)
        {
            var campaign = campaignsRepo.Get(campaignId);
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
            new CampaignRuns().Commit(viewModel.CampaignId);
            return RedirectToAction("Show", new { id = viewModel.CampaignId });
        }

        public FileResult DownloadMailingList(int id)
        {
            var membersRepo = new Members();
            var members = membersRepo.FromCampaignRun(id);

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
            var campaign = campaignsRepo.Get(id);
            var campaignClone = new Campaign
                                         {
                                             Id = 0,
                                             Description = campaign.Description + " (" + Resources.Literal_Cloned + ")",
                                             IsClosed = "N",
                                             Name = campaign.Name + " (" + Resources.Literal_Cloned + ")",
                                             RunOn = null
                                         };

            campaignClone = campaignsRepo.Insert(campaignClone);

            IEnumerable<MemberSearchFilter> memberSearchFilters = memberSearchFilterRepo.ForCampaign(id);

            foreach (MemberSearchFilter memberSearchFilter in memberSearchFilters)
            {
                memberSearchFilterRepo.Insert(new MemberSearchFilter
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
