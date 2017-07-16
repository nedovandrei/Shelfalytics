using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Repositories;

namespace Shelfalytics.API.Controllers
{
    [RoutePrefix("api/Auth")]
    public class AuthController : ApiController
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            if (authRepository == null) throw new ArgumentNullException(nameof(authRepository));
            _authRepository = authRepository;
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<HttpResponseMessage> RegisterUser(UserDTO user)
        {
            var result = await _authRepository.RegisterUser(user);
            return Request.CreateResponse(result);
        }

        [AllowAnonymous]
        [Route("Login")]
        public async Task<HttpResponseMessage> Login(UserLoginDTO user)
        {
            var result = await _authRepository.FindUser(user);
            return Request.CreateResponse(result);
        }
    }
}
