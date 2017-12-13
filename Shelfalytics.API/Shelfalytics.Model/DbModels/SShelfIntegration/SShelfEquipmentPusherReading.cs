using System.ComponentModel.DataAnnotations;

namespace Shelfalytics.Model.DbModels.SShelfIntegration
{
    public class SShelfEquipmentPusherReading
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int EquipmentReadingId { get; set; }
        [Required]
        public string PusherId { get; set; }
        public int Status { get; set; }
        [Required]
        public int Percentage { get; set; }
        public int Balance { get; set; }
        public int Error { get; set; }

    }
}
