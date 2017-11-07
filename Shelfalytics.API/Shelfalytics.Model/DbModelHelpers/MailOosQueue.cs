using System;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;

namespace Shelfalytics.Model.DbModelHelpers
{
    [TableName("MailOosQueue")]
    public class MailOosQueue
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int EquipmentId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }
    }
}
