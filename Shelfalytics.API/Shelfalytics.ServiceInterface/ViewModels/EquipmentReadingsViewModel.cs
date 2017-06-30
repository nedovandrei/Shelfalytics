using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.ServiceInterface.ViewModels
{
    public class EquipmentReadingsViewModel
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string PointOfSaleName { get; set; }
        public string PointOfSaleAddress { get; set; }
        public string PointOfSaleTelephone { get; set; }
        public string EquipmentType { get; set; }
        public string ModelName { get; set; }
        public int Temperature { get; set; }
        public int OpenCloseCountToday { get; set; }
        public int RowCount { get; set; }
        public IEnumerable<EquipmentRowInfoViewModel> RowInfo { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
