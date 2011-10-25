using System;
using System.Collections.Generic;
using GiveCRM.Models;
using GiveCRM.Web.Controllers;
using GiveCRM.Web.Services;
using MvcContrib.TestHelper;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;

namespace GiveCRM.Web.Tests.controllers
{
    [TestFixture]
    public class DonationControllerTests
    {
        private IDonationsService _donationsService;

        [SetUp]
        public void SetUp()
        {
            _donationsService = Substitute.For<IDonationsService>();
        }

        [Test]
        public void Index_Action_Returns_View()
        {
            var controller = new DonationController(_donationsService);
            var result = controller.Index();

            result.AssertViewRendered();
        }

        [Test]
        public void TopDonations_Action_Returns_View_With_A_List_Donations()
        {
            _donationsService.GetTopDonations().Received().Returns(new List<Donation>());

            var controller = new DonationController(_donationsService);
            var result = controller.TopDonations();

            result.AssertViewRendered().WithViewData<IEnumerable<Donation>>();

        }

        [Test]
        public void LatestDonations_Action_Returns_View_With_A_List_Donations()
        {
            _donationsService.GetLatestDonations().Received().Returns(new List<Donation>());

            var controller = new DonationController(_donationsService);
            var result = controller.LatestDonations();

            result.AssertViewRendered().WithViewData<IEnumerable<Donation>>();

        }

        [Test]
        public void QuickDonate_Action_Returns_View_With_An_Integer()
        {
            var id = 1;

            var controller = new DonationController(_donationsService);
            var result = controller.QuickDonate(id);

            result.AssertViewRendered().WithViewData<int>();
        }

        [Test]
        public void DoQuickDonate_Action_Returns_View()
        {
            var id = 1;
            var amount = 100;
            var date = "10/11/2011";
            int campaignId = 14;

            var controller = new DonationController(_donationsService);
            var result = controller.DoQuickDonate(id, amount, date, campaignId);

            result.AssertViewRendered();
        }

    }
}
