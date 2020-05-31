using System;
using System.Web.Http;
using System.Web.Http.SelfHost;

#region NOTES
// [IMPORTANT] URL Registration must be enabled.
//      - PPPPP = port in server config
//      - CN = computer name
//      - UN = username
// [CMD] netsh http add urlacl url=http://+:PPPPP/user=CN\UN

// .exe @ ../bin/debug

// [TIP] Pin a shortcut to task bar as any changes will require service to be stopped,
//      built, and restarted.
#endregion

namespace SelfHost
{
    class Program
    {
        static void Main()
        {
            #region ##### SERVER CONFIGURATION #####
            Uri _baseAddress = new Uri("http://localhost:60064/");
            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration(_baseAddress);

            config.Routes.MapHttpRoute(
              name: "DefaultApi",
              routeTemplate: "api/{controller}/{action}/{id}",
              defaults: new { id = RouteParameter.Optional }
            );
            #endregion

            #region #####  CREATE SERVER #####
            HttpSelfHostServer server = new HttpSelfHostServer(config);
            #endregion

            #region ##### RUN SERVER #####
            server.OpenAsync().Wait();
                Console.WriteLine("Electrify NZ Web-API Self hosted on " + _baseAddress);
                Console.WriteLine("Hit ENTER to exit...");
                Console.ReadLine();
            server.CloseAsync().Wait();
            #endregion
        }
    }
}
