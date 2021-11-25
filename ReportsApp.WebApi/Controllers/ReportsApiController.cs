using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using Newtonsoft.Json;
using ReportsApp.WebApi.ControllerExecution;
using ReportsApp.WebApi.Controllers.Dto;

namespace ReportsApp.WebApi.Controllers
{
    public class ReportsApiController : IApiController
    {
        public Dictionary<string, (Func<IApiChainParameter, ApiChainExecutionResult>, bool)> GetActionInfo()
            => new Dictionary<string, (Func<IApiChainParameter, ApiChainExecutionResult>, bool)>
            {
                {"Generate", (Generate, true)}
            };

        public string GetControllerName() => "ReportsApi";
        
        public ApiChainExecutionResult Generate(IApiChainParameter parameter)
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
                    JsonSerializer.Serialize(registeredUser));
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