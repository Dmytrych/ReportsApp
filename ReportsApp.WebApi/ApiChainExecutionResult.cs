using System.Collections.Generic;
using System.Net;

namespace ReportsApp.WebApi
{
    public class ApiChainExecutionResult : IApiChainExecutionResult
    {
        public string Result { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public List<Cookie> Cookies { get; set; }

        public bool Success { get; set; }
    }
}