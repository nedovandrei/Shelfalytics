using System.Collections.Generic;
using System.Threading.Tasks;
using Shelfalytics.RepositoryInterface.DTO;

namespace Shelfalytics.ServiceInterface
{
    public interface IPointOfSaleService
    {
        Task<IEnumerable<PointOfSaleDataDTO>> GetPointOfSaleData(int posId);
    }
}
