using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shelfalytics.API.Models.SShelfModels
{
    public class SShelfSaleInfo
    {
        public int Id { get; set; } // product id
        public int Delta { get; set; } // sale count
    }
}