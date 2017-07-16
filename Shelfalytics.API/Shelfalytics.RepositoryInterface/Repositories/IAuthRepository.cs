﻿using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shelfalytics.RepositoryInterface.DTO;

namespace Shelfalytics.RepositoryInterface.Repositories
{
    public interface IAuthRepository
    {
        Task<IdentityResult> RegisterUser(UserDTO user);
        Task<IdentityUser> FindUser(UserLoginDTO user);
    }
}