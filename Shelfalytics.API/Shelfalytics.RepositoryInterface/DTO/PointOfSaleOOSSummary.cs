using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO
{
    public class PointOfSaleOOSSummary
    {
        public int PointOfSaleId { get; set; }
        public string PointOfSaleName { get; set; }
        public double OOSPercentage { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PointOfSaleAddress { get; set; }
    }
}
