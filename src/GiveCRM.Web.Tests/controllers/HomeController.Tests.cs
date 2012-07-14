namespace GiveCRM.Web.Tests.controllers
{
    using GiveCRM.Web.Controllers;
    using MvcContrib.TestHelper;
    using NUnit.Framework;

    [TestFixture]
    public class HomeControllerTests
    {
        [Test]
        public void Home_Action_Returns_View_When_Called()
        {
            var controller = new HomeController();
            var result = controller.Index();

            result.AssertViewRendered();
        }

        [Test]
        public void About_Action_Returns_View_When_Called()
        {
            var controller = new HomeController();
            var result = controller.About();

            result.AssertViewRendered();
        }
    }
}
