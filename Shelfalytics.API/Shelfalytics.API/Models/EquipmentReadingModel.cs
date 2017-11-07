using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shelfalytics.API.Models
{
    public class EquipmentReadingModel
    {
        public string IMEI { get; set; }
        public decimal Temperature { get; set; }
        public IEnumerable<EquipmentDistanceReadingModel> DistanceSensors { get; set; }
        public bool IsPoweredOn { get; set; }
    }
}