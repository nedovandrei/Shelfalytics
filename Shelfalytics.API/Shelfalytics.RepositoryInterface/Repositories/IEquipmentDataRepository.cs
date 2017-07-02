using System.Collections.Generic;
using System.Threading.Tasks;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Helpers;

namespace Shelfalytics.RepositoryInterface.Repositories
{
    public interface IEquipmentDataRepository
    {
        Task<IEnumerable<EqiupmentDataDTO>> GetLatestEquipmentData(int equipmentId);
        Task<IEnumerable<EqiupmentDataDTO>> GetFilteredEquipmentData(int equimentId, GlobalFilter filter);
        Task<IEnumerable<int>> GetPointOfSaleEquipment(int posId);
    }
}
