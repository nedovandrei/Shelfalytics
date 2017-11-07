using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using Shelfalytics.Repository;
using Shelfalytics.Repository.Repositories;
using Shelfalytics.RepositoryInterface.DTO;

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

            var user = await _repo.FindUser(loginUser);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            await _repo.RegisterLoginDate(user.Id);

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //identity.AddClaim(new Claim("sub", context.UserName));
            //identity.AddClaim(new Claim("role", user.Roles.First().RoleId));
            identity.AddClaims(new List<Claim>
            {
                new Claim("sub", context.UserName),
                new Claim("role", user.Roles.First()?.RoleId),
                new Claim("name", user.EmployeeName),
                new Claim("phone", user.PhoneNumber),
                new Claim("id", user.Id),
                new Claim("clientId", user.ClientId.ToString()),
                new Claim("generalManagerId", user.GeneralManagerId != null ? user.GeneralManagerId : ""),
                new Claim("supervisorId", user.SupervisorId != null ? user.SupervisorId : "")
            });

            context.Validated(identity);

        }
    }
}