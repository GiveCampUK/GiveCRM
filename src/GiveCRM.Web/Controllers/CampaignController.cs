using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GiveCRM.DataAccess;
using GiveCRM.Models;
using GiveCRM.Web.Models.Campaigns;
using GiveCRM.Web.Models.Search;
using GiveCRM.Web.Properties;

namespace GiveCRM.Web.Controllers
{
    public class CampaignController : Controller
    {
        public ActionResult Index(bool showClosed = false)
        {
            var campaigns = new Campaigns();
            IEnumerable<Campaign> openCampaigns = showClosed ? campaigns.AllClosed() : campaigns.AllOpen();

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
                                Campaigns = openCampaigns
                            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CampaignShowViewModel(Resources.Literal_CreateCampaign);
            return View("Show", model);
        }

        [HttpPost]
        public ActionResult Create(Campaign campaign)
        {
            new Campaigns().Insert(campaign);
            return RedirectToAction("Show");
        }

        [HttpGet]
        public ActionResult Show(int id)
        {
            var campaignRepo = new Campaigns();
            var memberSearchFilterRepo = new MemberSearchFilters();
            var campaign = campaignRepo.Get(id);

            var model = new CampaignShowViewModel(Resources.Literal_ShowCampaign)
                            {
                                Campaign = campaign,
                                SearchFilters = memberSearchFilterRepo.ForCampaign(id).Select(
                                m => 
                                    new MemberSearchFilterViewModel
                                        {
                                            MemberSearchFilterId = m.Id,
                                            CampaignId = campaign.Id,
                                            CriteriaDisplayText = new SearchCriteria
                                                                      {
                                                                            InternalName = m.InternalName,
                                                                            DisplayName = m.DisplayName,
                                                                            Type = (SearchFieldType) m.FilterType,
                                                                            SearchOperator = (SearchOperator) m.SearchOperator,
                                                                            Value = m.Value
                                                                      }.ToString()
                                        }).ToList(),
                                NoSearchFiltersText = Resources.Literal_NoSearchFiltersText
                            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Show(Campaign campaign)
        {
            new Campaigns().Update(campaign);
            return View(campaign);
        }

        [HttpGet]
        public ActionResult AddMembershipSearchFilter(int campaignId)
        {
            var emptySearchCriteria = new SearchService().GetEmptySearchCriteria();
            var criteriaNames = emptySearchCriteria.Select(c => c.DisplayName);
            var searchOperators = ((SearchOperator[]) Enum.GetValues(typeof(SearchOperator)));

            var model = new AddSearchFilterViewModel(Resources.Literal_AddSearchFilter)
                            {
                                CampaignId = campaignId,
                                CriteriaNames = criteriaNames.Select(s => new SelectListItem {Value = s, Text = s}),
                                SearchOperators = searchOperators.Select(o => new SelectListItem {Value = o.ToString(), Text = o.ToString()})
                            };
            return View("AddSearchFilter", model);
        }

        [HttpPost]
        public ActionResult AddMembershipSearchFilter(AddSearchFilterViewModel viewModel)
        {
            var memberSearchFilterRepo = new MemberSearchFilters();
            var memberSearchFilter = new MemberSearchFilter
                                             {
                                                CampaignId = viewModel.CampaignId,
                                                InternalName = "??",
                                                DisplayName = viewModel.CriteriaName,
                                                SearchOperator = (int) viewModel.SearchOperator,
                                                Value = viewModel.Value
                                             };
            memberSearchFilterRepo.Insert(memberSearchFilter);
            return RedirectToAction("Show", new {id = viewModel.CampaignId});
        }

        [HttpGet]
        public ActionResult DeleteMemberSearchFilter(int campaignId, int memberSearchFilterId)
        {
            var memberSearchFilterRepo = new MemberSearchFilters();
            memberSearchFilterRepo.Delete(memberSearchFilterId);
            return RedirectToAction("Show", new {id = campaignId});
        }
    }
}
