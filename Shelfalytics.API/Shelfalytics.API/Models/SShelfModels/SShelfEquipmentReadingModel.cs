using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shelfalytics.API.Models.SShelfModels
{
    public class SShelfEquipmentReadingModel
    {
        public string Modem { get; set; } // modem ID
        public int Signal { get; set; } // signal levels
        public string Power { get; set; } // "Y" or "N" depending on power
        public int Bat { get; set; } // battery level
        public string Imei { get; set; } // modem imei number
        public string Loctype { get; set; } // location services supported
        public double Gps_long { get; set; } // gps longitude
        public double Gps_lat { get; set; } // gps latitude
        public int Gps_time { get; set; } // gps time
        public double Gsm_long { get; set; } // gsm longitude
        public double Gsm_lat { get; set; } // gsm latitude
        public int Gsm_time { get; set; } // gsm time
        public int Accel { get; set; } // TODO: ACQUIRE INFO accelerometer (?)
        public double Ax { get; set; } // TODO: ACQUIRE INFO accelerometer x axis (?)
        public double Ay { get; set; } // TODO: ACQUIRE INFO accelerometer y axis (?)
        public double Az { get; set; } // TODO: ACQUIRE INFO accelerometer z axis (?)
        public double T1 { get; set; } // TODO: ACQUIRE INFO idk
        public double T2 { get; set; } // TODO: ACQUIRE INFO idk
        public IEnumerable<SShelfPusher> Pushers { get; set; } // pusher list
        public IEnumerable<SShelfSaleInfo> Marks { get; set; } // sales info list

    }
}