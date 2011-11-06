using System.Collections.Generic;
using GiveCRM.Models;
using GiveCRM.Web.Controllers;
using GiveCRM.Web.Models.Facets;
using GiveCRM.Web.Services;
using MvcContrib.TestHelper;
using NSubstitute;
using NUnit.Framework;

namespace GiveCRM.Web.Tests.controllers
{
    [TestFixture]
    public class SetUpControllerTests
    {
        private IFacetsService facetService;

        [SetUp]
        public void SetUp()
        {
            facetService = Substitute.For<IFacetsService>();
        }

        [Test]
        public void Index_Action_Returns_View()
        {
            var controller = new SetupController(facetService);
            var result = controller.Index();

            result.AssertViewRendered();
        }

        [Test]
        public void AddFacet_Action_Returns_View_With_A_Facet()
        {
            var controller = new SetupController(facetService);
            var result = controller.AddFacet();

            result.AssertViewRendered().ForView("EditFacet").WithViewData<Facet>();
        }

        [Test]
        public void EditFacet_Action_Returns_View_With_A_Facet()
        {
            facetService.Get(1).Returns(new Facet());

            var controller = new SetupController(facetService);
            var result = controller.AddFacet();

            result.AssertViewRendered().ForView("EditFacet").WithViewData<Facet>();
        }

        [Test]
        public void SaveFacet_Action_With_New_Facet_Inserts_And_Redirects_To_Action()
        {
            facetService.Insert(new Facet());

            var controller = new SetupController(facetService);
            var result = controller.SaveFacet(new Facet());

            result.AssertActionRedirect();
        }

        [Test]
        public void SaveFacet_Action_With_Exsting_Facet_Inserts_And_Redirects_To_Action()
        {
            facetService.Insert(new Facet{Id = 20});

            var controller = new SetupController(facetService);
            var result = controller.SaveFacet(new Facet
                                                  {
                                                      Id = 20
                                                  });

            result.AssertActionRedirect();
        }

        [Test]
        public void ListFacets_Action_Returns_View_With_A_ViewModel()
        {
            facetService.All().Returns(new List<Facet>());

            var controller = new SetupController(facetService);
            var result = controller.ListFacets();

            result.AssertViewRendered().WithViewData<FacetListViewModel>();
        }
    }
}
