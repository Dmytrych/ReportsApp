using System.Net;
using ReportsApp.WebApi.Routing;

namespace ReportsApp.WebApi
{
    public interface IApiChainParameter
    {
        public RoutingResult RoutingResult { get; }
        
        public HttpListenerRequest Request { get; }

        public bool LoggedIn { get; set; }
    }
}