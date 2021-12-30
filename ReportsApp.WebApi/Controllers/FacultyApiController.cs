using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using ReportsApp.WebApi.ControllerExecution;
using ReportsApp.WebApi.Controllers.Services;

namespace ReportsApp.WebApi.Controllers
{
    public class FacultyApiController : IApiController
    {
        public Dictionary<string, (Func<IApiChainParameter, ApiChainExecutionResult>, bool)> GetActionInfo()
            => new Dictionary<string, (Func<IApiChainParameter, ApiChainExecutionResult>, bool)>
            {
                {"getAll", (GetAll, false)}
            };

        public string GetControllerName() => "FacultyApi";

        private readonly IDormitoryService _dormitoryService;

        public FacultyApiController(IDormitoryService dormitoryService)
        {
            _dormitoryService = dormitoryService;
        }
        
        public ApiChainExecutionResult GetAll(IApiChainParameter parameter)
        {
            var dormitories = _dormitoryService.GetAll();
            
            return dormitories == null 
                ? CreateRequest(HttpStatusCode.BadRequest)
                : CreateRequest(
                    HttpStatusCode.OK,
                    JsonSerializer.Serialize(dormitories));
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