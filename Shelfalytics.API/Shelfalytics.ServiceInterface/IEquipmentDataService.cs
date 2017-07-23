using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.ServiceInterface.ViewModels;

namespace Shelfalytics.ServiceInterface
{
    public interface IEquipmentDataService
    {
        Task<IEnumerable<EquipmentReadingsViewModel>> GetLatestEquipmentData(int equipmentId);
        Task RegisterReading(EquipmentReadingDTO reading);
    }
}
