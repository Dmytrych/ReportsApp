using System.Net;

namespace ReportsApp.WebApi.Server
{
    public interface IRequestHandler
    {
        public void Handle(HttpListenerContext context);
    }
}