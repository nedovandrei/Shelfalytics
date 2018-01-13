using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shelfalytics.Model.DbModelHelpers;

namespace Shelfalytics.Model.DbModels
{
    public class Equipment
    {
        [Key]
        public int Id { get; set; }
        public string IMEI { get; set; }
        [Required]
        public int PointOfSaleId { get; set; }
        [ForeignKey("PointOfSaleId")]
        public PointOfSale PointOfSale { get; set; }
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
        [Required]
        public int RowCount { get; set; }
        //[Required]
        //public int XCount { get; set; }
        [Required]
        public int YCount { get; set; }
        [Required]
        public int EmptyDistance { get; set; }
        [Required]
        public int FullDistance { get; set; }
        public int Width { get; set; }
        public string IntegrationModemId { get; set; }

    }
}
