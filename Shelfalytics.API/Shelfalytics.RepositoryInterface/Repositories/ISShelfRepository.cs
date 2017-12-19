using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shelfalytics.RepositoryInterface.DTO.SShelfIntegration;
using Shelfalytics.RepositoryInterface.DTO.SShelfIntegration.API;

namespace Shelfalytics.RepositoryInterface.Repositories
{
    public interface ISShelfRepository
    {
        Task Authorize();
        Task RegisterReading(SShelfEquipmentReadingDTO reading);

        #region SShelf API Calls

        Task<IEnumerable<SShelfStoreDTO>> GetSShelfStores();
        Task<IEnumerable<SShelfEquipmentDTO>> GetSShelfEquipment();
        Task<IEnumerable<SShelfBrandDTO>> GetSShelfProducts();
        Task<SShelfEquipmentDTO> GetSShelfEquipmentUnit(int id);

        #endregion
    }
}
