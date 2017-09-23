using Shelfalytics.RepositoryInterface.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.Repositories
{
    public interface IMailQueueRepository
    {
        Task<IEnumerable<MailOosQueueDTO>> GetProductMailQueue(int equipmentId, int productId);
        Task AddToQueue(MailOosQueueDTO mailQueue);
    }
}
