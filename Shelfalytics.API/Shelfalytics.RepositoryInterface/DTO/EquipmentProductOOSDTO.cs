using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO
{
    public class EquipmentProductOOSDTO
    {
        public int ProductId { get; set; }
        public int EquipmentId { get; set; }
        public int Row { get; set; }
        public string ProductName { get; set; }
        public string SKUName { get; set; }
        public double OOSPercentage { get; set; }
    }
}
