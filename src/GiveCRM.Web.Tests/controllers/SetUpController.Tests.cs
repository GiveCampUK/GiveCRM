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
        private IFacetsService _facetService;

        [SetUp]
        public void SetUp()
        {
            _facetService = Substitute.For<IFacetsService>();
        }

        [Test]
        public void Index_Action_Returns_View()
        {
            var controller = new SetupController(_facetService);
            var result = controller.Index();

            result.AssertViewRendered();
        }

        [Test]
        public void AddFacet_Action_Returns_View_With_A_Facet()
        {
            var controller = new SetupController(_facetService);
            var result = controller.AddFacet();

            result.AssertViewRendered().ForView("EditFacet").WithViewData<Facet>();
        }

        [Test]
        public void EditFacet_Action_Returns_View_With_A_Facet()
        {
            _facetService.Get(1).ReceivedWithAnyArgs().Returns(new Facet());

            var controller = new SetupController(_facetService);
            var result = controller.AddFacet();

            result.AssertViewRendered().ForView("EditFacet").WithViewData<Facet>();
        }

        [Test]
        public void ListFacets_Action_Returns_View_With_A_ViewModel()
        {
            _facetService.All().Received().Returns(new List<Facet>());

            var controller = new SetupController(_facetService);
            var result = controller.AddFacet();

            result.AssertViewRendered().WithViewData<FacetListViewModel>();
        }
    }
}
