namespace ReportsApp.WebApi.Authentication
{
    public class AuthenticationChainElement : ApiChainElementBase, IAuthenticationChainElement
    {
        public override ApiChainExecutionResult Execute(IApiChainParameter chainParameter)
        {
            var cookies = chainParameter.Request.Cookies;

            return ExecuteNext(chainParameter);
        }
    }
}