using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO
{
    public class LossesDueToOOSSummaryDTO
    {
        public decimal Total { get; set; }
        public IEnumerable<EquipmentLossesDueToOOSDTO> LossesByProducts { get; set; }
    }
}
