namespace ReportsApp.WebApi.Server
{
    public interface IServerEndpointListener
    {
        void Start();

        void Stop();
    }
}