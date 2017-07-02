using System.ComponentModel.DataAnnotations;

namespace Shelfalytics.Model.DbModelHelpers
{
    public class UserEquipmentTie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AgentUserId { get; set; }
        [Required]
        public int EquipmentId { get; set; }
    }
}
