using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string EAN { get; set; }
        public string SKUName { get; set; }
        public string ShortSKUName { get; set; }
        public string TradeMark { get; set; }
        public decimal Price { get; set; }
        public double Volume { get; set; }
        public string UnitOfMeasurement { get; set; }
        public int PackagingTypeId { get; set; }
        public double BottleDiameter { get; set; }
        public string PhotoPath { get; set; }
    }
}
