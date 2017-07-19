using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.DTO
{
    public class ExceptionLogDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Exception { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
