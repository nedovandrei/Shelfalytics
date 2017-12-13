using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO.SShelfIntegration
{
    public class SShelfEquipmentSalesReadingDTO
    {
        public int EquipmentReadingId { get; set; }
        public int ProductId { get; set; }
        public int SalesCount { get; set; }
    }
}
