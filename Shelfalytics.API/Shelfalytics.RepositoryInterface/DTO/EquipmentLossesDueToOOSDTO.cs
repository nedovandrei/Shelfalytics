using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO
{
    public class EquipmentLossesDueToOOSDTO
    {
        public string SKUName { get; set; }
        public string ShortSKUName { get; set; }
        public DateTime OOSStart { get; set; }
        public DateTime OOSEnd { get; set; }
        public TimeSpan TimePeriod { get; set; }
        public int TimePeriodDays { get; set; }
        public int TimePeriodHours { get; set; }
        public int TimePeriodMinutes { get; set; }
        public int TimePeriodSeconds { get; set; }
        public decimal Losses { get; set; }
        public decimal AverageSales { get; set; }
    }
}
