using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO
{
    public class EquipmentDetaildedOOSDTO
    {
        public List<EquipmentProductOOSDTO> OOSProducts { get; set; }
        public double TotalOOS { get; set; }
        public double ActualFill { get; set; }
    }
}
