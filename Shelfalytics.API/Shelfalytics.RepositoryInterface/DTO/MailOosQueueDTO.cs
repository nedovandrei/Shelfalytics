using System;

namespace Shelfalytics.RepositoryInterface.DTO
{
    public class MailOosQueueDTO
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public int ClientId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
