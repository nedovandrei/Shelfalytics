using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shelfalytics.Model.DbModelHelpers;

namespace Shelfalytics.Model.DbModels
{
    public class Equipment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        [Required]
        public int EquipmentTypeId { get; set; }
        [ForeignKey("EquipmentTypeId")]
        public EquipmentType EquipmentType { get; set; }
        [Required]
        [MaxLength(100)]
        public string ModelName { get; set; }
        public string Planogram { get; set; }
    }
}
