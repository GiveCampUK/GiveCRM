using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using GiveCRM.Web.Controllers;
using GiveCRM.Web.Services;
using NSubstitute;
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

            var request = Substitute.For<HttpRequestBase>();

                
            request.ApplicationPath.Returns("/"); 
            request.Url.Returns(new Uri("http://localhost/Home", UriKind.Absolute)); 
            request.ServerVariables.Returns(new System.Collections.Specialized.NameValueCollection());

            var response = Substitute.For<HttpResponseBase>();
                
            response.ApplyAppPathModifier("/Home").Returns("http://localhost/Home"); 
 
            var context = Substitute.For<HttpContextBase>(); 
            context.Request.Returns(request); 
            context.Response.Returns(response); 
 
            var controller = new AccountController(null,null,null);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);
            controller.Url = new UrlHelper(new RequestContext(context, new RouteData()), routes);
            
            var service = CreateService();
            var result = service.IsRedirectable(controller, @"/Home");
            Expect(result, Is.True);
        }

        [Test]
        public void ShouldReturnFalseForEmptyStringUrl()
        {
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);
var request = Substitute.For<HttpRequestBase>();


            request.ApplicationPath.Returns("/");
            request.Url.Returns(new Uri("http://localhost/Home", UriKind.Absolute));
            request.ServerVariables.Returns(new System.Collections.Specialized.NameValueCollection());

            var response = Substitute.For<HttpResponseBase>();

            response.ApplyAppPathModifier("/Home").Returns("http://localhost/Home");

            var context = Substitute.For<HttpContextBase>();
            context.Request.Returns(request);
            context.Response.Returns(response); 

            var controller = new AccountController(null, null, null);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);
            controller.Url = new UrlHelper(new RequestContext(context, new RouteData()), routes);

            var service = CreateService();
            var result = service.IsRedirectable(controller, string.Empty);
            Expect(result, Is.False);
        }

        [Test]
        public void ShouldReturnFalseForUrlNotBeginingWithForwardSlash()
        {
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);
var request = Substitute.For<HttpRequestBase>();


            request.ApplicationPath.Returns("/");
            request.Url.Returns(new Uri("http://localhost/Home", UriKind.Absolute));
            request.ServerVariables.Returns(new System.Collections.Specialized.NameValueCollection());

            var response = Substitute.For<HttpResponseBase>();

            response.ApplyAppPathModifier("/Home").Returns("http://localhost/Home");

            var context = Substitute.For<HttpContextBase>();
            context.Request.Returns(request);
            context.Response.Returns(response); 

            var controller = new AccountController(null, null, null);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);
            controller.Url = new UrlHelper(new RequestContext(context, new RouteData()), routes);

            var service = CreateService();
            var result = service.IsRedirectable(controller, "muppet");
            Expect(result, Is.False);
        }

        [Test]
        public void ShouldReturnFalseForUrlBeginingWithDoubleForwardSlash()
        {
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

var request = Substitute.For<HttpRequestBase>();


            request.ApplicationPath.Returns("/");
            request.Url.Returns(new Uri("http://localhost/Home", UriKind.Absolute));
            request.ServerVariables.Returns(new System.Collections.Specialized.NameValueCollection());

            var response = Substitute.For<HttpResponseBase>();

            response.ApplyAppPathModifier("/Home").Returns("http://localhost/Home");

            var context = Substitute.For<HttpContextBase>();
            context.Request.Returns(request);
            context.Response.Returns(response); 
            var controller = new AccountController(null, null, null);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);
            controller.Url = new UrlHelper(new RequestContext(context, new RouteData()), routes);

            var service = CreateService();
            var result = service.IsRedirectable(controller, "//muppet");
            Expect(result, Is.False);
        }
    }
}
