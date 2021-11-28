using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using ReportsApp.WebApi.ControllerExecution;
using ReportsApp.WebApi.Controllers.Services;

namespace ReportsApp.WebApi.Controllers
{
    public class ReportsApiController : IApiController
    {
        private readonly IReportsGenerationService _reportsGenerationService;
        
        public Dictionary<string, (Func<IApiChainParameter, ApiChainExecutionResult>, bool)> GetActionInfo()
            => new Dictionary<string, (Func<IApiChainParameter, ApiChainExecutionResult>, bool)>
            {
                {"Generate", (Generate, false)}
            };

        public string GetControllerName() => "ReportsApi";

        public ReportsApiController(IReportsGenerationService reportsGenerationService)
        {
            _reportsGenerationService = reportsGenerationService;
        }
        
        public ApiChainExecutionResult Generate(IApiChainParameter parameter)
        {
            var report = _reportsGenerationService.Generate();

            return report == null 
                ? CreateRequest(HttpStatusCode.BadRequest)
                : CreateRequest(
                    HttpStatusCode.OK,
                    JsonSerializer.Serialize(report));
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