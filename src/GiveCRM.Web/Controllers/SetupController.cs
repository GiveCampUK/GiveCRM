using System.Web.Mvc;
using GiveCRM.DataAccess;
using GiveCRM.Models;

namespace GiveCRM.Web.Controllers
{
    using System.Collections.Generic;

    public class SetupController : Controller
    {
        private Facets _facetsDb = new Facets();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddFacet()
        {
            return View();
        }

        public ActionResult EditFacet(int id)
        {
            return View();
        }

        public ActionResult SaveFacet(Facet facet)
        {
            return View();
        }

        public ActionResult AddFacetOption(FacetValue facetValue)
        {
            return View();
        }

        public ActionResult ShowFacets()
        {
            IEnumerable<Facet> facets = _facetsDb.All();

            return View(facets);
        }

    }
}
