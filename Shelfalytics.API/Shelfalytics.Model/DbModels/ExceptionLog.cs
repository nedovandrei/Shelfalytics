using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.Model.DbModels
{
    public class ExceptionLog
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Exception { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
