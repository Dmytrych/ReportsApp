using System;
using System.Net;
using Ninject;
using ReportsApp.WebApi.Ninject;

namespace ReportsApp.WebApi.Server
{
    public class ServerEndpointListener : IServerEndpointListener
    {
        private const string ServerNameString = "http://*:8080/";
        private readonly HttpListener _listener;
        
        public ServerEndpointListener(
            HttpListener listener)
        {
            _listener = listener;
            RegisterPrefixes();
        }
        
        public void Start()
        {
            var appKernel = new StandardKernel();
            appKernel.Load(new WebApiNinjectModule());
            _listener.Start();
            while (true)
            {
                var context = _listener.GetContext();
                try
                {
                    appKernel.Get<IRequestHandler>().Handle(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 500;
                    context.Response.Close();
                }
            }
        }

        public void Stop()
        {
            _listener.Stop();
        }

        private void RegisterPrefixes()
        { 
            _listener.Prefixes.Add(ServerNameString);
        }
    }
}