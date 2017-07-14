using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shelfalytics.Model.DbModels;
using Shelfalytics.RepositoryInterface;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Repositories;

namespace Shelfalytics.Repository.Repositories
{
    public class AuthRepository: IAuthRepository
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly UserManager<User> _userManager;

        public AuthRepository(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null) throw new ArgumentNullException(nameof(unitOfWorkFactory));
            _unitOfWorkFactory = unitOfWorkFactory;
            _userManager = new UserManager<User>(new UserStore<User>(unitOfWorkFactory.GetShelfalyticsIdentityDbContext()));
        }

        public async Task<IdentityResult> RegisterUser(UserDTO user)
        {
            var newUser = new User
            {
                UserName = user.UserName,
                EmployeeName = user.EmployeeName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                ClientId = user.ClientId,
                GeneralManagerId = user.GeneralManagerId,
                SupervisorId = user.SupervisorId,
            };

            try
            {
                var result = await _userManager.CreateAsync(newUser, user.Password);
                return result;
            }
            catch (Exception ex)
            {
                var exception = ex;
                return null;
            }
            
        }

        public async Task<IdentityUser> FindUser(UserLoginDTO user)
        {
            var result = await _userManager.FindAsync(user.UserName, user.Password);
            return result;
        }
    }
}
