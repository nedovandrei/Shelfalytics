using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO
{
    public class SalesSummaryDTO
    {
        public IEnumerable<EquipmentProductSalesDTO> Products { get; set; }
        public int SalesCount { get; set; }
    }
}
