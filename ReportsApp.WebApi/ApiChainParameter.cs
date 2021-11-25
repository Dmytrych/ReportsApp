using System.Net;
using ReportsApp.WebApi.Routing;

namespace ReportsApp.WebApi
{
    public class ApiChainParameter : IApiChainParameter
    {
        public ApiChainParameter(
            HttpListenerRequest request,
            RoutingResult routingResult)
        {
            Request = request;
            RoutingResult = routingResult;
        }

        public RoutingResult RoutingResult { get; }
        public HttpListenerRequest Request { get; }

        public bool LoggedIn { get; set; }
    }
}