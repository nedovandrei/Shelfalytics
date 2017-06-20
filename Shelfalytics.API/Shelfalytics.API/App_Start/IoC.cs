using System.Web.Http;

namespace Shelfalytics.API.App_Start
{
    public class IoC
    {
        public static void Run()
        {
            SetAutofacWebAPIServices();
        }

        private static void SetAutofacWebAPIServices()
        {
            var configuration = GlobalConfiguration.Configuration;
            configuration.DependencyResolver = StartupIoC.ConfigureIoC();
        }
    }
}