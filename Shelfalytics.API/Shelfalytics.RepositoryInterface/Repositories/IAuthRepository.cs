using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shelfalytics.Model.DbModels;
using Shelfalytics.RepositoryInterface.DTO;
using System.Collections.Generic;

namespace Shelfalytics.RepositoryInterface.Repositories
{
    public interface IAuthRepository
    {
        Task<IdentityResult> RegisterUser(UserDTO user);
        Task<User> FindUser(UserLoginDTO user);
        Task<IEnumerable<UserDTO>> GetUsersByClientId(int clientId);
    }
}
