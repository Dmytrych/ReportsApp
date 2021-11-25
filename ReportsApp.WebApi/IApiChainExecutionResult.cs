namespace ReportsApp.WebApi
{
    public interface IApiChainExecutionResult
    {
        public string Result { get; set; }
        
        public bool Success { get; set; }
    }
}