using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using Shelfalytics.Repository;
using Shelfalytics.Repository.Repositories;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Repositories;
using Microsoft.AspNet.Identity;

namespace Shelfalytics.API.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {


            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var _repo = new AuthRepository(new UnitOfWorkFactory());
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
           
            var loginUser = new UserLoginDTO
            {
                UserName = context.UserName,
                Password = context.Password
            };

            IdentityUser user = await _repo.FindUser(loginUser);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);

        }
    }
}