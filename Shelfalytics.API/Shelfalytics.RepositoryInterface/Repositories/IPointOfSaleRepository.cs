using System.Collections.Generic;
using System.Threading.Tasks;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Helpers;

namespace Shelfalytics.RepositoryInterface.Repositories
{
    public interface IPointOfSaleRepository
    {
        Task<IEnumerable<PointOfSaleDataDTO>> GetPointOfSaleData(int posId);
        Task<IEnumerable<PointOfSaleDataDTO>> GetPointsOfSales(int clientId, bool isAdmin);
        Task<IEnumerable<int>> GetPosEquipment(int posId, GlobalFilter filter);
    }
}
