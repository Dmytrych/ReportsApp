namespace ReportsApp.WebApi
{
    public abstract class ApiChainElementBase : IApiChainElement
    {
        private IApiChainElement NextChainElement { get; set; }

        public abstract ApiChainExecutionResult Execute(IApiChainParameter chainParameter);

        protected ApiChainExecutionResult ExecuteNext(IApiChainParameter chainParameter)
            => NextChainElement?.Execute(chainParameter);
    }
}