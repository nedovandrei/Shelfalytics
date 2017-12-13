using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.Model.DbModels.SShelfIntegration
{
    public class SShelfEquipmentSalesReading
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int EquipmentReadingId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public int SalesCount { get; set; }
    }
}
