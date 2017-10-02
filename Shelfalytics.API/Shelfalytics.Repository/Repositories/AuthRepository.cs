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
using Shelfalytics.RepositoryInterface.Helpers;
using System.Data.Entity;

namespace Shelfalytics.Repository.Repositories
{
    public class AuthRepository: IAuthRepository
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthRepository(IUnitOfWorkFactory unitOfWorkFactory)
        {
            
            if (unitOfWorkFactory == null) throw new ArgumentNullException(nameof(unitOfWorkFactory));
            _unitOfWorkFactory = unitOfWorkFactory;
            _userManager = new UserManager<User>(new UserStore<User>(unitOfWorkFactory.GetShelfalyticsIdentityDbContext()));

            var roleStore = new RoleStore<IdentityRole>(unitOfWorkFactory.GetShelfalyticsIdentityDbContext());

            _roleManager = new RoleManager<IdentityRole>(roleStore);
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
                var role = _roleManager.FindById(user.Role);
                var result = await _userManager.CreateAsync(newUser, user.Password);
                await _userManager.AddToRoleAsync(newUser.Id, role.Name);
                return result;
            }
            catch (Exception ex)
            {
                var exception = ex;
                return null;
            }
        }

        public async Task<User> FindUser(UserLoginDTO user)
        {
            var result = await _userManager.FindAsync(user.UserName, user.Password);
            return result;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersByClientId(int clientId)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsIdentityDbContext())
            {
                var query = from user in uow.Set<User>()
                            where user.ClientId == clientId
                            select new UserDTO
                            {
                                Id = user.Id,
                                ClientId = user.ClientId,
                                Email = user.Email,
                                EmployeeName = user.EmployeeName,
                                GeneralManagerId = user.GeneralManagerId,
                                PhoneNumber = user.PhoneNumber,
                                Role = user.Roles.FirstOrDefault().RoleId,
                                SupervisorId = user.SupervisorId,
                                UserName = user.UserName
                            };
                return await query.ToListAsync();
            }
        }
    }
}
