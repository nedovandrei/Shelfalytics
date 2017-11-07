using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Routing;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Shelfalytics.API.App_Start;
using Shelfalytics.API.Handlers;
using Shelfalytics.API.Providers;
using Thinktecture.IdentityModel;
using System.Web.Cors;
using System.Threading.Tasks;

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
            var policy = new CorsPolicy
            {
                AllowAnyHeader = true,
                AllowAnyMethod = true,
                AllowAnyOrigin = true,
                SupportsCredentials = true
            };

            app.UseCors(new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(policy)
                }
            });

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
            var issuer = Constants.Issuer;
            var audience = Constants.Audience;
            var secret = Convert.FromBase64String(Constants.Secret);

            var OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(5),
                Provider = new SimpleAuthorizationServerProvider(),
                //AccessTokenFormat = new MyJwtFormat()
            };
            OAuthServerOptions.AccessTokenFormat = new MyJwtFormat(OAuthServerOptions);

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions()
            {
                AllowedAudiences = new [] { audience },
                AuthenticationMode = AuthenticationMode.Active,
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                    }
            });
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }

    internal static class Constants
    {
        public static string Issuer = ConfigurationManager.AppSettings["Issuer"];
        public static string Audience = ConfigurationManager.AppSettings["AudienceId"];
        public static string Secret = ConfigurationManager.AppSettings["AudienceSecret"];
    }

    public class MyJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly OAuthAuthorizationServerOptions _options;

        public MyJwtFormat(OAuthAuthorizationServerOptions options)
        {
            _options = options;
        }

        public string SignatureAlgorithm
        {
            get { return "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256"; }
        }

        public string DigestAlgorithm
        {
            get { return "http://www.w3.org/2001/04/xmlenc#sha256"; }
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null) throw new ArgumentNullException("data");

            var issuer =Constants.Issuer;
            var audience = Constants.Audience;
            var key = Convert.FromBase64String(Constants.Secret);
            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(_options.AccessTokenExpireTimeSpan.TotalMinutes);
            var signingCredentials = new SigningCredentials(
                                        new InMemorySymmetricSecurityKey(key),
                                        SignatureAlgorithm,
                                        DigestAlgorithm);
            var token = new JwtSecurityToken(issuer, audience, data.Identity.Claims,
                                             now, expires, signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}
