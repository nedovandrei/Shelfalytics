using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shelfalytics.Model.DbModels
{
    public class EquipmentDistanceReading
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int EquipmentReadingId { get; set; }
        [ForeignKey("EquipmentReadingId")]
        public EquipmentReading EquipmentReading { get; set; }
        [Required]
        public int Distance { get; set; }
        [Required]
        public int Row { get; set; }
    }
}
