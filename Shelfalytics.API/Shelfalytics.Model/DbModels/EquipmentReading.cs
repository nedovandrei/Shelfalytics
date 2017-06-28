using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shelfalytics.Model.DbModels
{
    public class EquipmentReading
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int EquipmentId { get; set; }
        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
        public DateTime TimeSpamp { get; set; }
        public bool WasOpened { get; set; }
        public int Temperature { get; set; }
    }
}
