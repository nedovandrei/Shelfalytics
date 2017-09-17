using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO
{
    public class ProductOOSDTO
    {
        public int ProductId { get; set; }
        public int EquipmentId { get; set; }
        public int Row { get; set; }
        public string ProductName { get; set; }
        public string SKUName { get; set; }
        public string ShortSKUName { get; set; }
        public double BottleDiameter { get; set; }
        public decimal Price { get; set; }
        public string PhotoPath { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}
