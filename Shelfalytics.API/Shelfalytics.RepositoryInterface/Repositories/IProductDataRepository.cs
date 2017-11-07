using System.Collections.Generic;
using System.Threading.Tasks;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Helpers;

namespace Shelfalytics.RepositoryInterface.Repositories
{
    public interface IProductDataRepository
    {
        Task<IEnumerable<EquipmentPlanogramDTO>> GetEquipmentPlanogram(int equipmentId);
        Task<IEnumerable<ProductDTO>> GetClientsProducts(int clientId, bool isAdmin);
        Task<IEnumerable<ProductDTO>> GetClientsProducts(ExportFilter filter);
    }
}
