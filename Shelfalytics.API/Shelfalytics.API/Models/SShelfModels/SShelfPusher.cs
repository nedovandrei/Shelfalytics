using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shelfalytics.API.Models.SShelfModels
{
    public class SShelfPusher
    {
        public string Id { get; set; } // Pusher id 
        public int Status { get; set; } // pusher status code
        public int Percent { get; set; } // pusher fullness percentage
        public int Balance { get; set; } // undelivered products count (?)
        public int Error { get; set; } // pusher error status, 0 - if no error, 1 - if error
    }
}