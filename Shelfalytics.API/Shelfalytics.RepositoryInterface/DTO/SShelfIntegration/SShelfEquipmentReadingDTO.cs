using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shelfalytics.Model.DbModels.SShelfIntegration;

namespace Shelfalytics.RepositoryInterface.DTO.SShelfIntegration
{
    public class SShelfEquipmentReadingDTO
    {
        public string ModemId { get; set; }
        public bool Power { get; set; }
        public int Signal { get; set; }
        public int BatteryLevel { get; set; }
        public double GpsLongitude { get; set; }
        public double GpsLatitude { get; set; }
        public double GsmLongitude { get; set; }
        public double GsmLatitude { get; set; }
        public IEnumerable<SShelfEquipmentPusherReading> Pushers { get; set; }
        public IEnumerable<SShelfEquipmentSalesReading> Marks { get; set; }
    }
}
