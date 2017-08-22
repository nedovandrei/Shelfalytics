using System.Collections.Generic;
using System.Threading.Tasks;
using Shelfalytics.Model.DbModels;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Helpers;

namespace Shelfalytics.RepositoryInterface.Repositories
{
    public interface IEquipmentDataRepository
    {
        Task<IEnumerable<EqiupmentDataDTO>> GetLatestEquipmentData(int equipmentId);
        Task<IEnumerable<EqiupmentDataDTO>> GetFilteredEquipmentData(int equimentId, GlobalFilter filter);
        Task<IEnumerable<int>> GetPointOfSaleEquipment(int posId);
        Task<IEnumerable<EquipmentDTO>> GetEquipments();
        Task<EquipmentDTO> GetEquipmentByIMEI(string imei);
        Task<EquipmentReading> RegisterEquipmentReading(EquipmentReading reading);
        Task<EquipmentReadingGetDTO> GetLatestReading(int equipmentId);
        Task RegisterEquipmentDistanceReadings(IEnumerable<EquipmentDistanceReading> distanceReadings);
        Task<bool> EquipmentHasReadings(int equipmentId);
    }
}
