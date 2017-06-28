using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shelfalytics.Model.DbModels
{
    public class EquipmentPlanogram
    {
        [Key]
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        [Required]
        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
        [Required]
        public int Row { get; set; }
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
