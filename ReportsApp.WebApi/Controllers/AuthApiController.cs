using System;
using System.Collections.Generic;
using System.Net;
using ReportsApp.Authentication.Dto;
using ReportsApp.WebApi.ControllerExecution;
using ReportsApp.WebApi.Controllers.Dto;
using ReportsApp.WebApi.Controllers.Services;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ReportsApp.WebApi.Controllers
{
    public class AuthApiController : IApiController
    {
        private const string AuthCookieName = "AuthToken";
        private readonly IAuthService _authService;
        
        public Dictionary<string, (Func<IApiChainParameter, ApiChainExecutionResult>, bool)> GetActionInfo()
            => new Dictionary<string, (Func<IApiChainParameter, ApiChainExecutionResult>, bool)>
            {
                {"Register", (Register, false)},
                {"Login", (Login, false)}
            };

        public string GetControllerName() => "AuthApi";

        public AuthApiController(IAuthService authService)
        {
            _authService = authService;
        }

        public ApiChainExecutionResult Register(IApiChainParameter parameter)
        {
            var studentParseResult = IApiController.ParseDto<UserClientDto>(parameter.Request.InputStream);

            if (!studentParseResult.isValid)
            {
                return CreateRequest(HttpStatusCode.BadRequest);
            }

            var registeredUser = _authService.Register(studentParseResult.result);

            return registeredUser == null 
                ? CreateRequest(HttpStatusCode.BadRequest)
                : CreateRequest(HttpStatusCode.OK, JsonSerializer.Serialize(registeredUser));
        }

        public ApiChainExecutionResult Login(IApiChainParameter parameter)
        {
            var studentParseResult = IApiController.ParseDto<UserCredentialsDto>(parameter.Request.InputStream);

            if (!studentParseResult.isValid)
            {
                return CreateRequest(HttpStatusCode.BadRequest);
            }

            var registeredUser = _authService.Login(studentParseResult.result.Login, studentParseResult.result.Password);

            return registeredUser == null 
                ? CreateRequest(HttpStatusCode.BadRequest)
                : CreateRequest(
                    HttpStatusCode.OK,
                    JsonSerializer.Serialize(registeredUser),
                    new List<Cookie>
                    {
                        CreateAuthCookie(registeredUser.Login, registeredUser.Password)
                    });
        }

        private Cookie CreateAuthCookie(string login, string password)
        {
            var cookie = new Cookie(AuthCookieName, string.Concat(login, "\\", password));
            cookie.HttpOnly = true;
            cookie.Expires = DateTime.Now.Add(TimeSpan.FromDays(1));
            cookie.Secure = true;
            cookie.Path = "";
            return cookie;
        }

        private ApiChainExecutionResult CreateRequest(
            HttpStatusCode code,
            string content = null,
            List<Cookie> cookies = null)
            => new()
            {
                Cookies = cookies ?? new List<Cookie>(),
                Result = content ?? string.Empty,
                StatusCode = code,
                Success = true
            };
    }
}