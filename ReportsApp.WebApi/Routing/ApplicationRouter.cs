using System;
using System.Linq;

namespace ReportsApp.WebApi.Routing
{
    public class ApplicationRouter : IApplicationRouter
    {
        public RoutingResult GetRoute(Uri uri)
        {
            var segments = uri.Segments.Select(segment => segment.TrimEnd('/')).ToArray();

            if (segments.Length != 3)
            {
                return null;
            }

            return new RoutingResult
            {
                ControllerName = segments[1],
                ControllerAction = segments[2]
            };
        }
    }
}