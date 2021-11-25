using ReportsApp.WebApi.Routing;

namespace ReportsApp.WebApi.ControllerExecution
{
    public interface IControllerActionResolver
    {
        public ControllerActionInfo Resolve(RoutingResult result);
    }
}