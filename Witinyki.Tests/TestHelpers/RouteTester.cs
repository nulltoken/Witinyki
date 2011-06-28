using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Web.Mvc;
using MvcContrib.TestHelper;
using MvcContrib.TestHelper.Fakes;
using NUnit.Framework;

namespace Witinyki.Tests.TestHelpers
{
    public class RouteTester<TController> where TController : Controller
    {
        private readonly FakeHttpContext _context;
        private readonly Func<TController> _controllerBuilder;
        private readonly RequestContext _requestContext;

        public RouteTester(Func<TController> controllerBuilder)
        {
            _controllerBuilder = controllerBuilder;
            _context = new FakeHttpContext("~/");
            _requestContext = new RequestContext(_context, new RouteData());
        }

        public ActionResult Ensure(string url, Expression<Func<TController, ActionResult>> expression)
        {
            url.ShouldMapTo(expression);

            var action = expression.Compile();
            return action(_controllerBuilder());
        }

        public void ResultRedirects(ActionResult res, string expectedUrl)
        {
            res.AssertActionRedirect();
            Assert.AreEqual(expectedUrl, GetRedirectUrl(res));
        }

        public string GetLinkFor(Expression<Action<TController>> expression)
        {
            return LinkBuilder.BuildUrlFromExpression(_requestContext, RouteTable.Routes,
                                                      expression);
        }

        private string GetRedirectUrl(ActionResult actionResult)
        {
            Assert.IsInstanceOf<RedirectToRouteResult>(actionResult);
            var redirectToRouteResult = (RedirectToRouteResult) actionResult;

            return UrlHelper.GenerateUrl(redirectToRouteResult.RouteName, null, null, redirectToRouteResult.RouteValues,
                                         RouteTable.Routes, _requestContext, false);
        }
    }
}