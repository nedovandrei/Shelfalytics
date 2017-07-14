using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public int ClientId { get; set; }
        public string PhoneNumber { get; set; }
        public int GeneralManagerId { get; set; }
        public int SupervisorId { get; set; }
    }
}
