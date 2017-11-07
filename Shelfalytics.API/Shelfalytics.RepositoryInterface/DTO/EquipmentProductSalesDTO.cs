using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO
{
    public class EquipmentProductSalesDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ShortProductName { get; set; }
        public string TradeMark { get; set; }
        public int Sales { get; set; }
    }
}
