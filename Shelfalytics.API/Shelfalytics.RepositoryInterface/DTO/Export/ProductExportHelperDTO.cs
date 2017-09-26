using Shelfalytics.Model.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO.Export
{
    public class ProductExportHelperDTO
    {
        public int Id { get; set; }
        public string SKUName { get; set; }
        public string ShortSKUName { get; set; }
        public IEnumerable<Equipment> RelatedEquipments { get; set; }
        public IEnumerable<Sale> Sales { get; set; }
        public IEnumerable<EquipmentReading> EquipmentReadings { get; set; }
        public IEnumerable<EquipmentDistanceReading> EquipmentDistanceReadings { get; set; }
    }
}
