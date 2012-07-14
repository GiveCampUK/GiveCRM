﻿namespace GiveCRM.Web.Tests.controllers
{
    using System.Collections.Generic;
    using GiveCRM.BusinessLogic;
    using GiveCRM.Models;
    using GiveCRM.Web.Controllers;
    using GiveCRM.Web.Models.Members;
    using MvcContrib.TestHelper;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class MemberControllerTests
    {
        private IDonationsService donationsService;
        private IMemberService memberService;
        private ICampaignService campaignService;

        [SetUp]
        public void SetUp()
        {
            campaignService = Substitute.For<ICampaignService>();
            donationsService = Substitute.For<IDonationsService>();
            memberService = Substitute.For<IMemberService>();
        }

        [Test]
        public void Index_Action_Returns_View()
        {
            var controller = new MemberController(donationsService, memberService, campaignService);
            var result = controller.Index();

            result.AssertViewRendered();
        }

        [Test]
        public void Add_Action_Returns_View_With_ViewModel()
        {
            var controller = new MemberController(donationsService, memberService, campaignService);
            var result = controller.Add();

            result.AssertViewRendered().WithViewData<MemberEditViewModel>();
        }

        [Test]
        public void Edit_Action_Returns_Named_View_With_ViewModel()
        {
            memberService.Get(1).Returns(new Member());

            var controller = new MemberController(donationsService, memberService, campaignService);
            var result = controller.Edit(1);

            Assert.That(result.AssertViewRendered().ViewName == "Add");
            result.AssertViewRendered().WithViewData<MemberEditViewModel>();
        }

        [Test]
        public void Delete_Action_Returns_RedirectToAction()
        {
            memberService.Get(1).Returns(new Member());
            memberService.Update(new Member());

            var controller = new MemberController(donationsService, memberService, campaignService);
            var result = controller.Delete(1);

            result.AssertActionRedirect().ToAction("Index");
        }

        [Test]
        public void Donate_Action_Returns_View_With_Model()
        {
            memberService.Get(1).Returns(new Member());
            campaignService.GetAllOpen().Returns(new List<Campaign>());

            var controller = new MemberController(donationsService, memberService, campaignService);
            var result = controller.Donate(1);

            result.AssertViewRendered().WithViewData<Donation>();
        }

        [Test]
        public void QuickDonation_Action_RedirectsToAction()
        {
            donationsService.QuickDonation(new Donation());

            var controller = new MemberController(donationsService, memberService, campaignService);
            var result = controller.SaveDonation(new Donation());

            result.AssertActionRedirect().ToAction("Index");
        }
    }
}
