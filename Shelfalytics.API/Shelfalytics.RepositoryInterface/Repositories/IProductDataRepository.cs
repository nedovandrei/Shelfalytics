using System.Collections.Generic;
using System.Threading.Tasks;
using Shelfalytics.RepositoryInterface.DTO;

namespace Shelfalytics.RepositoryInterface.Repositories
{
    public interface IProductDataRepository
    {
        Task<IEnumerable<EquipmentPlanogramDTO>> GetEquipmentPlanogram(int equipmentId);
    }
}
