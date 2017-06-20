using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO
{
    public class EqiupmentDataDTO
    {
        public int Id { get; set; }
        //public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string EquipmentType { get; set; }
        public string ModelName { get; set; }
        public string Planogram { get; set; }
        public int OpenCloseCount { get; set; }
        public int Temperature { get; set; }
        public int PercentageLine1 { get; set; }
        public int PercentageLine2 { get; set; }
        public int PercentageLine3 { get; set; }
    }
}
