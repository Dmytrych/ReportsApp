using ReportsApp.WebApi.ControllerExecution;

namespace ReportsApp.WebApi
{
    public class ApiChainCreator : IApiChainCreator
    {
        private readonly IControllerExecutionApiChainElement _controllerExecutionApiChainElement;
        
        public ApiChainCreator(IControllerExecutionApiChainElement controllerExecutionApiChainElement)
        {
            _controllerExecutionApiChainElement = controllerExecutionApiChainElement;
        }
        
        public IApiChainElement Create()
        {
            return _controllerExecutionApiChainElement;
        }
    }
}