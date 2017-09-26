using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.Helpers
{
    public class ExportFilter
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ClientId { get; set; }
        public bool IsAdmin { get; set; }
        public List<int> PointsOfSale { get; set; }
        public List<int> Equipments { get; set; }
        public List<int> Products { get; set; }
        public List<string> Cities { get; set; }
        public List<string> TradeChannels { get; set; }
        public List<string> ChainNames { get; set; }
        public string Locale { get; set; }
    }
}
