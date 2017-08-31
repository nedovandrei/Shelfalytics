using System.Collections.Generic;
using System.Threading.Tasks;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Helpers;

namespace Shelfalytics.ServiceInterface
{
    public interface IPointOfSaleService
    {
        Task<IEnumerable<PointOfSaleDataDTO>> GetPointOfSaleData(int posId, int clientId);
        Task<IEnumerable<PointOfSaleDataDTO>> GetPointsOfSales(GlobalFilter filter);
    }
}
