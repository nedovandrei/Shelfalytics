using System;

namespace Shelfalytics.RepositoryInterface.Helpers
{
    public class GlobalFilter
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ClientId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
