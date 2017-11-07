using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO.Export
{
    public class ProductExportDTO
    {
        public int Id { get; set; }
        public string SKUName { get; set; }
        public string ShortSKUName { get; set; }
        public decimal OOSInUnits { get; set; }
        public decimal OOSInMoney { get; set; }
        public int Sales { get; set; }
        public int DoorOpens { get; set; }
        public double SalesByOpen { get; set; }
    }
}
