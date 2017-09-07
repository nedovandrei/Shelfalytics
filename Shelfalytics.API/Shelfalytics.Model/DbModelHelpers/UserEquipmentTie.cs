using System.ComponentModel.DataAnnotations;

namespace Shelfalytics.Model.DbModelHelpers
{
    public class UserEquipmentTie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int EquipmentId { get; set; }
    }
}
