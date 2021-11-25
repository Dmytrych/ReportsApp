using System;

namespace ReportsApp.WebApi.Routing
{
    public interface IApplicationRouter
    {
        public RoutingResult GetRoute(Uri uri);
    }
}