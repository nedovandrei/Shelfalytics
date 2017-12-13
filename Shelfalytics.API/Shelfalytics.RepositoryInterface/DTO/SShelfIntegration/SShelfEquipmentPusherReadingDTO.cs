using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO.SShelfIntegration
{
    public class SShelfEquipmentPusherReadingDTO
    {
        public int EquipmentReadingId { get; set; }
        public string PusherId { get; set; }
        public int Status { get; set; }
        public int Percentage { get; set; }
        public int Balance { get; set; }
        public int Error { get; set; }
    }
}
