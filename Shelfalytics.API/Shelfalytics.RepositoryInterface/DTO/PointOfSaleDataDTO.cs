using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shelfalytics.Model.DbModels;

namespace Shelfalytics.RepositoryInterface.DTO
{
    public class PointOfSaleDataDTO
    {
        public int PointOfSaleId { get; set; }
        public string PointOfSaleName { get; set; }
        public string ChainName { get; set; }
        public string PointOfSaleAddress { get; set; }
        public string City { get; set; }
        public string TradeChannel { get; set; }
        public string PointOfSaleTelephone { get; set; }
        public string ContactPersonName { get; set; }
        public DateTime OpeningHours { get; set; }
        public DateTime ClosingHours { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IEnumerable<int> EquipmentIds { get; set; }
        public IEnumerable<Equipment> Equipment { get; set; }
    }
}
