using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shelfalytics.Model.DbModelHelpers;

namespace Shelfalytics.Model.DbModels
{
    [Table("Users")]
    public class User: IdentityUser
    {
        //[Key]
        //public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string EmployeeName { get; set; }
        //[Required]
        //public int RoleId { get; set; }
        //[ForeignKey("RoleId")]
        //public UserRole UserRole { get; set; }
        [Required]
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        [Required]
        [Phone]
        public override string PhoneNumber { get; set; }
        public int GeneralManagerId { get; set; }
        public int SupervisorId { get; set; }
    }
}
