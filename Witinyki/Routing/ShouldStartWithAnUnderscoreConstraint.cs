using System;
using System.Web;
using System.Web.Routing;

namespace Witinyki.Routing
{
    public class ShouldStartWithAnUnderscoreConstraint : IRouteConstraint
    {
        private readonly bool _shouldIt;

        public ShouldStartWithAnUnderscoreConstraint(bool shouldIt)
        {
            _shouldIt = shouldIt;
        }

        #region IRouteConstraint Members

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
                          RouteDirection routeDirection)
        {
            var value = values[parameterName] as string;

            if (string.IsNullOrEmpty(value))
            {
                return true;
            }

            bool startsWith = value.StartsWith("_", StringComparison.Ordinal);

            if (_shouldIt)
                return startsWith;

            return !startsWith;
        }

        #endregion
    }
}