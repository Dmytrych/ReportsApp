using System;
using System.Collections.Generic;
using System.Net;
using ReportsApp.Authentication.Dto;
using ReportsApp.WebApi.Authentication;

namespace ReportsApp.WebApi.ControllerExecution
{
    public class ControllerExecutionApiChainElement : IControllerExecutionApiChainElement
    {
        private const string AuthCookieName = "AuthToken";
        
        private readonly IControllerActionResolver _controllerActionResolver;

        private readonly IApplicationAuthenticator _applicationAuthenticator;
        
        public ControllerExecutionApiChainElement(
            IControllerActionResolver controllerActionResolver,
            IApplicationAuthenticator applicationAuthenticator)
        {
            _controllerActionResolver = controllerActionResolver;
            _applicationAuthenticator = applicationAuthenticator;
        }
        
        public ApiChainExecutionResult Execute(IApiChainParameter chainParameter)
        {
            var controllerInfo = _controllerActionResolver.Resolve(chainParameter.RoutingResult);

            if (controllerInfo == null)
            {
                return CreateInvalidResult(HttpStatusCode.NotFound);
            }

            if (!controllerInfo.AuthOnly)
            {
                return controllerInfo.Action.Invoke(chainParameter);
            }

            var user = Authenticate(chainParameter.Request.Cookies);

            return user == null 
                ? CreateInvalidResult(HttpStatusCode.Forbidden)
                : controllerInfo.Action.Invoke(chainParameter);
        }

        private UserClientDto Authenticate(CookieCollection cookies)
        {
            var authCookie = cookies[AuthCookieName];

            if (authCookie == null || authCookie.Expired)
            {
                return null;
            }

            return _applicationAuthenticator.Authenticate(authCookie.Value);
        }

        private ApiChainExecutionResult CreateInvalidResult(HttpStatusCode statusCode)
            => new ApiChainExecutionResult()
            {
                StatusCode = statusCode,
                Cookies = new List<Cookie>(),
                Result = String.Empty,
                Success = false
            };
    }
}