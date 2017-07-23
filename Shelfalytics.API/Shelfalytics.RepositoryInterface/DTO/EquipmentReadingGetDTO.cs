using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shelfalytics.Model.DbModels;

namespace Shelfalytics.RepositoryInterface.DTO
{
    public class EquipmentReadingGetDTO
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public DateTime TimeSpamp { get; set; }
        public int Temperature { get; set; }
        public IEnumerable<EquipmentDistanceReading> SensorReadings { get; set; }
    }
}
