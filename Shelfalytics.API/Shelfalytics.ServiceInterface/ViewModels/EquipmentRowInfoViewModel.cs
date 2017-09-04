using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.ServiceInterface.ViewModels
{
    public class EquipmentRowInfoViewModel
    {
        public int Row { get; set; }
        public double Percentage { get; set; }
        public string ProductName { get; set; }
        public string SKUName { get; set; }
        public double BottleDiameter { get; set; }
        public string PhotoPath { get; set; }
    }
}
