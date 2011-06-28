using System.Web.Mvc;
using System.Web.Routing;

namespace Witinyki.Routing
{
    public static class Registrar
    {
        public static void RegisterWikiRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "e",
                "wiki",
                new {controller = "Wiki", action = "Index"}
                );

            routes.MapRoute(
                "a2",
                "wiki/{pageName}/{version}",
                new {controller = "Wiki", action = "Browse", version = UrlParameter.Optional},
                new
                    {
                        pageName = new ShouldStartWithAnUnderscoreConstraint(false),
                        version = new ShouldStartWithAnUnderscoreConstraint(false),
                    }
                );

            routes.MapRoute(
                "b",
                "wiki/{pageName}/{action}",
                new {controller = "Wiki"},
                new
                    {
                        pageName = new ShouldStartWithAnUnderscoreConstraint(false),
                        action = new ShouldStartWithAnUnderscoreConstraint(true),
                    }
                );

            routes.MapRoute(
                "d",
                "wiki/{action}",
                new {controller = "Wiki"},
                new
                    {
                        action = new ShouldStartWithAnUnderscoreConstraint(true),
                    }
                );
        }
    }
}