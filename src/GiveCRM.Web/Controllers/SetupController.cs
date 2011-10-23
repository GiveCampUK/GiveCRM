using System.Collections.Generic;
using System.Web.Mvc;
using GiveCRM.DataAccess;
using GiveCRM.Models;
using GiveCRM.Web.Models.Facets;

namespace GiveCRM.Web.Controllers
{
    public class SetupController : Controller
    {
        private Facets _facetsDb = new Facets();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddFacet()
        {
            return View(new Facet { Values = new List<FacetValue>() });
        }

        public ActionResult EditFacet(int id)
        {
            return View();
        }

        public ActionResult SaveFacet(Facet facet)
        {
            if (facet.Id == 0)
            {
                _facetsDb.Insert(facet);
            }
            else
            {
                _facetsDb.Update(facet);
            }
            return RedirectToAction("ListFacets");
        }

        public ActionResult AddFacetOption(FacetValue facetValue)
        {
            return View();
        }

        public ActionResult ListFacets()
        {
            var facets = new List<Facet>(_facetsDb.All());
            var viewModel = new FacetListViewModel
            {
                Facets = facets
            };

            return View(viewModel);
        }
    }
}
