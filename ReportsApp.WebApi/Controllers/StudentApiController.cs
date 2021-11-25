using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using ReportsApp.WebApi.ControllerExecution;
using ReportsApp.WebApi.Controllers.Services;
using ReportsApp.WebApi.Dto;

namespace ReportsApp.WebApi.Controllers
{
    public class StudentApiController : IApiController
    {
        private readonly IStudentService _studentService;
        
        public Dictionary<string, (Func<IApiChainParameter, ApiChainExecutionResult>, bool)> GetActionInfo()
            => new Dictionary<string, (Func<IApiChainParameter, ApiChainExecutionResult>, bool)>
            {
                {"Add", (Add, true)}
            };

        public string GetControllerName() => "StudentApi";

        public StudentApiController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public ApiChainExecutionResult Add(IApiChainParameter parameter)
        {
            var studentParseResult = IApiController.ParseDto<StudentClientDto>(parameter.Request.InputStream);

            if (!studentParseResult.isValid)
            {
                return new ApiChainExecutionResult()
                {
                    Cookies = new List<Cookie>(),
                    Result = "",
                    StatusCode = HttpStatusCode.BadRequest,
                    Success = true
                };
            }

            var resultStudent = _studentService.AddStudent(studentParseResult.result);

            return new ApiChainExecutionResult()
            {
                Cookies = new List<Cookie>(),
                Result = JsonSerializer.Serialize(resultStudent),
                StatusCode = HttpStatusCode.OK,
                Success = true
            };
        }
    }
}