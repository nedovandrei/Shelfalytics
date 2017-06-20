using System.Collections.Generic;
using System.Threading.Tasks;
using Shelfalytics.RepositoryInterface.DTO;

namespace Shelfalytics.RepositoryInterface.Repositories
{
    public interface IEquipmentDataRepository
    {
        Task<IEnumerable<EqiupmentDataDTO>> GetLatestEquipmentData(int equipmentId);
    }
}
