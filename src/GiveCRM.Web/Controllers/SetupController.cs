using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GiveCRM.DataAccess;
using GiveCRM.Models;
using GiveCRM.Web.Models.Facets;
using GiveCRM.Web.Services;

namespace GiveCRM.Web.Controllers
{
    public class SetupController : Controller
    {
        private IFacetsService _facetService;

        public SetupController(IFacetsService facetsService)
        {
            _facetService = facetsService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddFacet()
        {
            var facet = new Facet
                {
                    Values = new List<FacetValue>()
                };
            return View("EditFacet", facet);
        }

        public ActionResult EditFacet(int id)
        {
            var facet = _facetService.Get(id);
            return View("EditFacet", facet);
        }

        [HttpPost]
        public ActionResult SaveFacet(Facet facet)
        {
            CleanFacet(facet);

            if (facet.Id > 0)
            {
                _facetService.Update(facet);
            }
            else
            {
                _facetService.Insert(facet);            
            }

            return RedirectToAction("ListFacets");
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

        public ActionResult ListFacets()
        {
            var facets = new List<Facet>(_facetService.All());
            var viewModel = new FacetListViewModel
                {
                    Facets = facets
                };

            return View(viewModel);
        }
    }
}
