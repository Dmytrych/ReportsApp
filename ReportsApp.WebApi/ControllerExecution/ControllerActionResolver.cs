using System;
using System.Linq;
using ReportsApp.WebApi.Routing;

namespace ReportsApp.WebApi.ControllerExecution
{
    public class ControllerActionResolver : IControllerActionResolver
    {
        private readonly IApiController[] _controllers;

        public ControllerActionResolver(IApiController[] controllers)
        {
            _controllers = controllers;
        }
        
        public ControllerActionInfo Resolve(RoutingResult result)
        {
            var controller = _controllers.FirstOrDefault(ctr => string.Equals(ctr.GetControllerName(), result.ControllerName, StringComparison.InvariantCultureIgnoreCase));

            if (controller == null || !controller.GetActionInfo().Keys.Contains(result.ControllerAction))
            {
                return null;
            }
            
            var (action, authOnly) = controller.GetActionInfo()[result.ControllerAction];
            return new ControllerActionInfo(controller, action, authOnly);
        }
    }
}