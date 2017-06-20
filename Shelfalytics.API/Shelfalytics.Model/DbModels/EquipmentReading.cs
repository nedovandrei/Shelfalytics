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
        public int OpenCloseCount { get; set; }
        public int Temperature { get; set; }
        public int Distance1 { get; set; }
        public int Distance2 { get; set; }
        public int Distance3 { get; set; }
        //and so on


    }
}
