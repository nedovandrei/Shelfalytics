using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Routing;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Shelfalytics.API.App_Start;
using Shelfalytics.API.Handlers;
using Shelfalytics.API.Providers;
using Thinktecture.IdentityModel;

[assembly: OwinStartup("Application_Startup", typeof(Shelfalytics.API.WebApiApplication))]
namespace Shelfalytics.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public void Application_Start(IAppBuilder app)
        {
            Configuration(app);
            //var config = new HttpConfiguration();
            //app.UseWebApi(config);
            //IoC.Run();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            //GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        }

        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            ConfigureOAuth(app);
            
            var config = new HttpConfiguration();
            //config.Services.Replace(typeof(IExceptionHandler), new CustomExceptionHandler());
            //config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionHandler());
            app.UseWebApi(config);
            
            IoC.Run();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
