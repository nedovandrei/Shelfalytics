using System.Collections.Generic;
using System.Threading.Tasks;
using Shelfalytics.RepositoryInterface.DTO;

namespace Shelfalytics.RepositoryInterface.Repositories
{
    public interface IPointOfSaleRepository
    {
        Task<IEnumerable<PointOfSaleDataDTO>> GetPointOfSaleData(int posId);
        Task<IEnumerable<PointOfSaleDataDTO>> GetPointsOfSales();
        Task<IEnumerable<int>> GetPosEquipment(int posId);
    }
}
