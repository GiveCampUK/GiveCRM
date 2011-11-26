using System.Collections.Generic;
using GiveCRM.BusinessLogic;
using GiveCRM.Models;
using GiveCRM.Web.Controllers;
using GiveCRM.Web.Models.Donation;
using MvcContrib.TestHelper;
using NSubstitute;
using NUnit.Framework;

namespace GiveCRM.Web.Tests.controllers
{
    [TestFixture]
    public class DonationControllerTests
    {
        private IDonationsService donationsService;
        private ICampaignService campaignService;

        [SetUp]
        public void SetUp()
        {
            donationsService = Substitute.For<IDonationsService>();
            campaignService = Substitute.For<ICampaignService>();
        }

        [Test]
        public void Index_Action_Returns_View()
        {
            var controller = new DonationController(donationsService, campaignService);
            var result = controller.Index();

            result.AssertViewRendered();
        }

        [Test]
        public void TopDonations_Action_Returns_View_With_A_List_Donations()
        {
            donationsService.GetTopDonations(5).Received().Returns(new List<Donation>());

            var controller = new DonationController(donationsService, campaignService);
            var result = controller.TopDonations();

            result.AssertViewRendered().WithViewData<IEnumerable<Donation>>();

        }

        [Test]
        public void LatestDonations_Action_Returns_View_With_A_List_Donations()
        {
            donationsService.GetLatestDonations(5).Received().Returns(new List<Donation>());

            var controller = new DonationController(donationsService, campaignService);
            var result = controller.LatestDonations();

            result.AssertViewRendered().WithViewData<IEnumerable<Donation>>();

        }

        [Test]
        public void QuickDonate_Action_Returns_View_With_A_QuickDonateViewModel()
        {
            const int id = 1;

            var controller = new DonationController(donationsService, campaignService);
            var result = controller.QuickDonate(id);

            result.AssertViewRendered().WithViewData<QuickDonateViewModel>();
        }

        [Test]
        public void DoQuickDonate_Action_Returns_View()
        {
            const int id = 1;
            const int amount = 100;
            const string date = "10/11/2011";
            const int campaignId = 14;

            var controller = new DonationController(donationsService, campaignService);
            var result = controller.DoQuickDonate(id, amount, date, campaignId);

            result.AssertViewRendered();
        }

    }
}
