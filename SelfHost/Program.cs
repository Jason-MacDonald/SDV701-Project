using System;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace SelfHost
{
    class Program
    {
        static void Main()
        {
            #region SERVER CONFIGURATION
            // Set Configuration.
            Uri _baseAddress = new Uri("http://localhost:60064/");
            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration(_baseAddress);

            config.Routes.MapHttpRoute(
              name: "DefaultApi",
              routeTemplate: "api/{controller}/{action}/{id}",
              defaults: new { id = RouteParameter.Optional }
            );

            // Create server.
            HttpSelfHostServer server = new HttpSelfHostServer(config);
            #endregion

            #region RUN SERVER
            // Start listening.
            server.OpenAsync().Wait();

            Console.WriteLine("Electrify NZ Web-API Self hosted on " + _baseAddress);
            Console.WriteLine("Hit ENTER to exit...");
            Console.ReadLine();

            server.CloseAsync().Wait();
            #endregion
        }
    }
}
