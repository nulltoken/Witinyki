using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Witinyki.Routing;

namespace Witinyki
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            Registrar.RegisterWikiRoutes(routes);

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Parameter defaults
                );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            EnsureWikiRepoHasBeenCreated();
        }

        private static void EnsureWikiRepoHasBeenCreated()
        {
            if (WikiRepoHelper.HasWikiRepoBeenCreated())
            {
                return;
            }

            WikiRepoHelper.BuildSampleWikiRepo();
        }
    }
}