using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shelfalytics.Model.DbModelHelpers;

namespace Shelfalytics.Model.DbModels
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string EmployeeName { get; set; }
        [Required]
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public UserRole UserRole { get; set; }
        [Required]
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        [Required]
        [Phone]
        public string Telephone { get; set; }
        public int GeneralManagerId { get; set; }
        public int SupervisorId { get; set; }
    }
}
