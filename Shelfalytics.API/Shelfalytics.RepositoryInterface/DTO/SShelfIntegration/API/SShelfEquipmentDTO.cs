using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO.SShelfIntegration.API
{
    public class SShelfEquipmentDTO
    {
        public int Id { get; set; }

        /// <summary>
        /// POS Id in which unit is stored
        /// </summary>
        public int Id_store { get; set; }

        /// <summary>
        /// Serial number of equipment unit
        /// </summary>
        public string Serial { get; set; }

        /// <summary>
        /// Equipment name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Column quantity
        /// </summary>
        public int Cols { get; set; }

        /// <summary>
        /// Row quantity
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// Notes about the unit (?)
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Phone number of the sim intalled
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Our dearest friends from Kazahstan thought that we won't require explanation about this field (WTF)
        /// </summary>
        public string Phonedate { get; set; }

        /// <summary>
        /// Same here
        /// </summary>
        public string Paymonth { get;set; }

        /// <summary>
        /// I mean, what the fuck guys
        /// </summary>
        public string Simserial { get; set; }

        /// <summary>
        /// Timestamp of the planogram change
        /// </summary>
        public DateTime Plan_date { get; set; }

        /// <summary>
        /// Trade equipment type ID
        /// </summary>
        public int Id_type { get; set; }

        /// <summary>
        /// Timestamp of last registered data input from device
        /// </summary>
        public DateTime? Ping { get; set; }

        /// <summary>
        /// Latest photo taken by equipment
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Account code in vendor's system (what?)
        /// </summary>
        public string Store_code { get; set; }
    }
}
