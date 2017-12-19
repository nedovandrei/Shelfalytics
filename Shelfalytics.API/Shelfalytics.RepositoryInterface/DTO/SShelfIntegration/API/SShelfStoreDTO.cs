using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO.SShelfIntegration.API
{
    public class SShelfStoreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Code { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public string Addr { get; set; } // Address

        /// <summary>
        /// Longitude
        /// </summary>
        public double L { get; set; } // Longitude

        /// <summary>
        /// Latitude
        /// </summary>
        public double W { get; set; } // Latitude

        /// <summary>
        /// Manager id in SShelf system
        /// </summary>
        public int Manager { get; set; } // manager ID

        /// <summary>
        /// Opening hour (?)
        /// </summary>
        public int H_Start { get; set; } 

        /// <summary>
        /// Closing hour (?)
        /// </summary>
        public int H_End { get; set; }

        /// <summary>
        /// Last registered ping from equipment
        /// </summary>
        public DateTime Last_ping { get; set; }

        /// <summary>
        /// Number of units of equipment installed
        /// </summary>
        public int Units { get; set; }
    }
}
