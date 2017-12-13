using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.Model.DbModels.SShelfIntegration
{
    public class SShelfEquipmentReading
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ModemId { get; set; }
        public int Signal { get; set; }
        public int BatteryLevel { get; set; }
        public double GpsLongitude { get; set; }
        public double GpsLatitude { get; set; }
        public double GsmLongitude { get; set; }
        public double GsmLatitude { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
