using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using GiveCRM.Web.Controllers;
using GiveCRM.Web.Services;
using Moq;
using NUnit.Framework;
using System.Web.Routing;

namespace GiveCRM.Web.Tests.Services
{
    [TestFixture]
    public class UrlValidationServiceTest:AssertionHelper
    {
        
        private UrlValidationService CreateService()
        {
            var rules = new List<IAmAUrlValidationRule>
                            {
                                new IsLocal(),
                                new LengthIsGreaterThanOne(),
                                new BeginsWithForwardSlash(),
                                new DoesNotBeginWithDoubleForwardSlash(),
                                new DoesNotBeginWithForwardSlashBackslash()
                            };
            return new UrlValidationService(rules);
        }

        [Test]
        public void ShouldReturnTrueForValidUrl()
        {
            var routes = new RouteCollection(); 
            MvcApplication.RegisterRoutes(routes); 
 
            var request = new Mock<HttpRequestBase>(MockBehavior.Strict); 
            request.SetupGet(x => x.ApplicationPath).Returns("/"); 
            request.SetupGet(x => x.Url).Returns(new Uri("http://localhost/Home", UriKind.Absolute)); 
            request.SetupGet(x => x.ServerVariables).Returns(new System.Collections.Specialized.NameValueCollection()); 
 
            var response = new Mock<HttpResponseBase>(MockBehavior.Strict); 
            response.Setup(x => x.ApplyAppPathModifier("/Home")).Returns("http://localhost/Home"); 
 
            var context = new Mock<HttpContextBase>(MockBehavior.Strict); 
            context.SetupGet(x => x.Request).Returns(request.Object); 
            context.SetupGet(x => x.Response).Returns(response.Object); 
 
            var controller = new AccountController(null,null,null);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            controller.Url = new UrlHelper(new RequestContext(context.Object, new RouteData()), routes);
            
            var service = CreateService();
            var result = service.IsRedirectable(controller, @"/Home");
            Expect(result, Is.True);
        }

        [Test]
        public void ShouldReturnFalseForEmptyStringUrl()
        {
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            request.SetupGet(x => x.ApplicationPath).Returns("/");
            request.SetupGet(x => x.Url).Returns(new Uri("http://localhost/Home", UriKind.Absolute));
            request.SetupGet(x => x.ServerVariables).Returns(new System.Collections.Specialized.NameValueCollection());

            var response = new Mock<HttpResponseBase>(MockBehavior.Strict);
            response.Setup(x => x.ApplyAppPathModifier("/Home")).Returns("http://localhost/Home");

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context.SetupGet(x => x.Request).Returns(request.Object);
            context.SetupGet(x => x.Response).Returns(response.Object);

            var controller = new AccountController(null, null, null);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            controller.Url = new UrlHelper(new RequestContext(context.Object, new RouteData()), routes);

            var service = CreateService();
            var result = service.IsRedirectable(controller, string.Empty);
            Expect(result, Is.False);
        }

        [Test]
        public void ShouldReturnFalseForUrlNotBeginingWithForwardSlash()
        {
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            request.SetupGet(x => x.ApplicationPath).Returns("/");
            request.SetupGet(x => x.Url).Returns(new Uri("http://localhost/Home", UriKind.Absolute));
            request.SetupGet(x => x.ServerVariables).Returns(new System.Collections.Specialized.NameValueCollection());

            var response = new Mock<HttpResponseBase>(MockBehavior.Strict);
            response.Setup(x => x.ApplyAppPathModifier("/Home")).Returns("http://localhost/Home");

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context.SetupGet(x => x.Request).Returns(request.Object);
            context.SetupGet(x => x.Response).Returns(response.Object);

            var controller = new AccountController(null, null, null);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            controller.Url = new UrlHelper(new RequestContext(context.Object, new RouteData()), routes);

            var service = CreateService();
            var result = service.IsRedirectable(controller, "muppet");
            Expect(result, Is.False);
        }

        [Test]
        public void ShouldReturnFalseForUrlBeginingWithDoubleForwardSlash()
        {
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            request.SetupGet(x => x.ApplicationPath).Returns("/");
            request.SetupGet(x => x.Url).Returns(new Uri("http://localhost/Home", UriKind.Absolute));
            request.SetupGet(x => x.ServerVariables).Returns(new System.Collections.Specialized.NameValueCollection());

            var response = new Mock<HttpResponseBase>(MockBehavior.Strict);
            response.Setup(x => x.ApplyAppPathModifier("/Home")).Returns("http://localhost/Home");

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context.SetupGet(x => x.Request).Returns(request.Object);
            context.SetupGet(x => x.Response).Returns(response.Object);

            var controller = new AccountController(null, null, null);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            controller.Url = new UrlHelper(new RequestContext(context.Object, new RouteData()), routes);

            var service = CreateService();
            var result = service.IsRedirectable(controller, "//muppet");
            Expect(result, Is.False);
        }
    }
}
