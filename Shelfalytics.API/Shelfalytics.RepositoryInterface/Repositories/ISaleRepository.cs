using System.Collections.Generic;
using System.Threading.Tasks;
using Shelfalytics.Model.DbModels;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Helpers;

namespace Shelfalytics.RepositoryInterface.Repositories
{
    public interface ISaleRepository
    {
        Task RegisterSale(Sale sale);
        Task<IEnumerable<EquipmentProductSalesDTO>> GetEquipmentSales(int equipmentId, GlobalFilter filter);
        Task<IEnumerable<EquipmentProductSalesDTO>> GetProductSalesSummary(GlobalFilter filter);
    }
}
