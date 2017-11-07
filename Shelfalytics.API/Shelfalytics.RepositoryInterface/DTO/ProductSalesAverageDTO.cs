using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO
{
    public class ProductSalesAverageDTO
    {
        public string ProductName { get; set; }
        public string ShortProductName { get; set; }
        public string TradeMark { get; set; }
        public decimal AverageSales { get; set; }
    }
}
