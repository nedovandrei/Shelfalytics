using Shelfalytics.RepositoryInterface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.Helpers
{
    public class ExportSelects
    {
        public IEnumerable<PointOfSaleDataDTO> PointsOfSale { get; set; }
        public IEnumerable<ProductDTO> Products { get; set; }
        public IEnumerable<EquipmentDTO> Equipments { get; set; }
        public List<string> Locals { get; set; }
        public IEnumerable<string> TradeChanels = new List<string> { "Traditional Trade", "Retail Chain", "Horeca" };
    }
}
