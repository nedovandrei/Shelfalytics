using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO
{
    public class EquipmentDTO
    {
        public int Id { get; set; }
        public string IMEI { get; set; }

        public int PointOfSaleId { get; set; }
        public int ClientId { get; set; }
        public int EquipmentTypeId { get; set; }
        public string ModelName { get; set; }
        public int RowCount { get; set; }
        //public int XCount { get; set; }
        public int YCount { get; set; }
        public int EmptyDistance { get; set; }
        public int FullDistance { get; set; }
        public int Width { get; set; }
    }
}
