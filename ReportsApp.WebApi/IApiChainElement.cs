using System.Net;

namespace ReportsApp.WebApi
{
    public interface IApiChainElement
    {
        ApiChainExecutionResult Execute(IApiChainParameter chainParameter);
    }
}