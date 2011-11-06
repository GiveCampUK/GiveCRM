
namespace GiveCRM.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using GiveCRM.BusinessLogic;
    using GiveCRM.Models;
    using GiveCRM.Web.Models.Facets;
    
    public class SetupController : Controller
    {
        private readonly IFacetsService facetService;

        public SetupController(IFacetsService facetsService)
        {
            this.facetService = facetsService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddFacet()
        {
            var facet = new Facet
                {
                    Values = new List<FacetValue>()
                };
            return View("EditFacet", facet);
        }

        [HttpGet]
        public ActionResult EditFacet(int id)
        {
            var facet = this.facetService.Get(id);
            return View("EditFacet", facet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveFacet(Facet facet)
        {
            CleanFacet(facet);

            if (facet.Id > 0)
            {
                this.facetService.Update(facet);
            }
            else
            {
                this.facetService.Insert(facet);            
            }

            return RedirectToAction("ListFacets");
        }

        [HttpGet]
        public ActionResult ListFacets()
        {
            var facets = new List<Facet>(this.facetService.All());
            var viewModel = new FacetListViewModel
            {
                Facets = facets
            };

            return View(viewModel);
        }

        /// <summary>
        /// bad Hack because the form is going wrong and values are not working
        /// discard the null ones
        /// </summary>
        /// <param name="facet"></param>
        private void CleanFacet(Facet facet)
        {
            if (facet.Values != null)
            {
                facet.Values = facet.Values.Where(fc => ! string.IsNullOrEmpty(fc.Value)).ToList();
            }
        }
    }
}
