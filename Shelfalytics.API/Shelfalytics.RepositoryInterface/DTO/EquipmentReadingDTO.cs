using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO
{
    public class EquipmentReadingDTO
    {
        public string IMEI { get; set; }
        public int Temperature { get; set; }
        public IEnumerable<EquipmentDistanceReadingDTO> DistanceSensors { get; set; }
        public bool IsPoweredOn { get; set; }
    }
}
