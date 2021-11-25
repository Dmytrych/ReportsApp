using System;

namespace ReportsApp.WebApi.ControllerExecution
{
    public class ControllerActionInfo
    {
        public ControllerActionInfo(
            IApiController controller,
            Func<IApiChainParameter, ApiChainExecutionResult> action,
            bool authOnly)
        {
            Controller = controller;
            Action = action;
            AuthOnly = authOnly;
        }

        public IApiController Controller { get; set; }
        
        public Func<IApiChainParameter, ApiChainExecutionResult> Action { get; set; }
        
        public bool AuthOnly { get; set; }
    }
}