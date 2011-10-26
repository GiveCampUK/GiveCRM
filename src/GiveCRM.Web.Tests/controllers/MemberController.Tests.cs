using System.Collections.Generic;
using GiveCRM.Models;
using GiveCRM.Web.Controllers;
using GiveCRM.Web.Models.Members;
using GiveCRM.Web.Services;
using MvcContrib.TestHelper;
using NSubstitute;
using NUnit.Framework;

namespace GiveCRM.Web.Tests.controllers
{
    [TestFixture]
    public class MemberControllerTests
    {
        private IDonationsService _donationsService;
        private IMemberService _memberService;
        private ICampaignService _campaignService;

        [SetUp]
        public void SetUp()
        {
            _campaignService = Substitute.For<ICampaignService>();
            _donationsService = Substitute.For<IDonationsService>();
            _memberService = Substitute.For<IMemberService>();
        }

        [Test]
        public void Index_Action_Returns_View()
        {
            var controller = new MemberController(_donationsService, _memberService, _campaignService);
            var result = controller.Index();

            result.AssertViewRendered();
        }

        [Test]
        public void Add_Action_Returns_View_With_ViewModel()
        {
            var controller = new MemberController(_donationsService, _memberService, _campaignService);
            var result = controller.Add();

            result.AssertViewRendered().WithViewData<MemberEditViewModel>();
        }

        [Test]
        public void Edit_Action_Returns_Named_View_With_ViewModel()
        {
            _memberService.Get(1).Returns(new Member());

            var controller = new MemberController(_donationsService, _memberService, _campaignService);
            var result = controller.Edit(1);

            result.AssertViewRendered().ForView("Add").WithViewData<MemberEditViewModel>();
        }

        [Test]
        public void Delete_Action_Returns_RedirectToAction()
        {
            _memberService.Get(1).Returns(new Member());
            _memberService.Update(new Member());

            var controller = new MemberController(_donationsService, _memberService, _campaignService);
            var result = controller.Delete(1);

            result.AssertActionRedirect().ToAction("Index");
        }

        [Test]
        public void Donate_Action_Returns_View_With_Model()
        {
            _memberService.Get(1).Returns(new Member());
            _campaignService.AllOpen().Returns(new List<Campaign>());

            var controller = new MemberController(_donationsService, _memberService, _campaignService);
            var result = controller.Donate(1);

            result.AssertViewRendered().WithViewData<Donation>();
        }

        [Test]
        public void QuickDonation_Action_RedirectsToAction()
        {
            _donationsService.QuickDonation(new Donation());

            var controller = new MemberController(_donationsService, _memberService, _campaignService);
            var result = controller.SaveDonation(new Donation());

            result.AssertActionRedirect().ToAction("Index");
        }

        [Test]
        public void TopDonors_Action_Returns_View_With_Model()
        {
            _memberService.All().Returns(new List<Member>());

            var controller = new MemberController(_donationsService, _memberService, _campaignService);
            var result = controller.TopDonors();

            result.AssertViewRendered().ForView("MembersList").WithViewData<IEnumerable<Member>>();
        }
    }
}
