using System.Net;
using Ninject;
using ReportsApp.WebApi.Ninject;
using ReportsApp.WebApi.Server;

namespace ReportsApp
{
    class Program
    {
        static void Main(string[] argss)
        {
            var kernel = new StandardKernel(new WebApiNinjectModule());
            var serverListener = kernel.Get<IServerEndpointListener>();
            kernel.Bind<HttpListener>().ToConstant(new HttpListener());

            serverListener.Start();
        }
    }
}