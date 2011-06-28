using System.IO;
using System.Web.Routing;
using Witinyki.Controllers;
using Witinyki.Routing;

namespace Witinyki.Tests.TestHelpers
{
    public class BaseFixture
    {
        private static readonly RouteTester<WikiController> _rt;

        static BaseFixture()
        {
            SetUpTestEnvironment();
            _rt = BuildRouteTester();
        }

        protected RouteTester<WikiController> RT { get { return _rt; } }

        private static void SetUpTestEnvironment()
        {
            var target = new DirectoryInfo(Constants.WorkingDirectory);

            if (target.Exists)
            {
                target.Delete(recursive: true);
            }

            WikiRepoHelper.BuildSampleWikiRepo();
        }

        private static RouteTester<WikiController> BuildRouteTester()
        {
            var routes = RouteTable.Routes;
            routes.Clear();

            Registrar.RegisterWikiRoutes(routes);

            return new RouteTester<WikiController>(() => new WikiController());
        }
    }
}