using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using ReportsApp.WebApi.Routing;

namespace ReportsApp.WebApi.Server
{
    public class RequestHandler : IRequestHandler
    {
        private readonly IApiChainCreator _apiChainCreator;
        private readonly IApplicationRouter _applicationRouter;
        
        public RequestHandler(
            IApiChainCreator apiChainCreator,
            IApplicationRouter applicationRouter)
        {
            _apiChainCreator = apiChainCreator;
            _applicationRouter = applicationRouter;
        }
        
        public void Handle(HttpListenerContext context)
        {
            var routingResult = _applicationRouter.GetRoute(context.Request.Url);

            if (routingResult == null)
            {
                SendResponse(context.Response, 404, String.Empty);
                return;
            }

            var chain = _apiChainCreator.Create();

            var result = chain.Execute(CreateChainParameter(context.Request, routingResult));
            
            SendResponse(context.Response, (int)result.StatusCode, result.Result, result.Cookies);
        }

        private void SendResponse(HttpListenerResponse response, int statusCode, string content, List<Cookie> cookies = null)
        {
            response.AddHeader("Access-Control-Allow-Origin", "http://localhost:3000");
            response.AddHeader("Access-Control-Allow-Credentials","true");
            cookies?.ForEach(cookie => response.Cookies.Add(cookie));
            response.ContentType = "application/json";
            response.StatusCode = statusCode;
            response.OutputStream.Write(Encoding.Default.GetBytes(content));
            response.OutputStream.Flush();
            response.Close();
        }

        private IApiChainParameter CreateChainParameter(HttpListenerRequest request, RoutingResult routingResult)
            => new ApiChainParameter(request, routingResult);
    }
}