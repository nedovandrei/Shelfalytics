using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Helpers;

namespace Shelfalytics.ServiceInterface
{
    public interface IStatisticsService
    {
        Task<EquipmentDetaildedOOSDTO> GetEquipmentOOS(int equipmentId, GlobalFilter filter);
        Task<EquipmentDetaildedOOSDTO> GetPOSOOS(int posId, GlobalFilter filter);
        Task<List<PointOfSaleOOSSummary>> GetPOSOOSSummary(GlobalFilter filter);
        Task<IEnumerable<EquipmentProductSalesDTO>> GetProductSalesData(int equipmentId, GlobalFilter filter);
        Task<EquipmentDetaildedOOSDTO> GetTopSkuOOS(GlobalFilter filter);
        Task<SalesSummaryDTO> GetProductSalesSummary(GlobalFilter filter);
        Task<IEnumerable<EquipmentLossesDueToOOSDTO>> GetEquipmentLossesDueToOos(int equipmentId, GlobalFilter filter);
        Task<LossesDueToOOSSummaryDTO> GetLossesDueToOOSSummary(GlobalFilter filter);
        Task<IEnumerable<BusinessDevelopersDTO>> GetTopBestBusinessDevelopers(GlobalFilter filter);
    }
}
