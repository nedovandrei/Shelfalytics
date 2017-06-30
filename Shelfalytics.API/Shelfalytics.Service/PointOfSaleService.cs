using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Repositories;
using Shelfalytics.ServiceInterface;

namespace Shelfalytics.Service
{
    public class PointOfSaleService : IPointOfSaleService
    {
        private readonly IPointOfSaleRepository _pointOfSaleRepository;
        private readonly IEquipmentDataRepository _equipmentDataRepository;

        public PointOfSaleService(IPointOfSaleRepository pointOfSaleRepository, IEquipmentDataRepository equipmentDataRepository)
        {
            if(pointOfSaleRepository == null) throw new ArgumentNullException(nameof(pointOfSaleRepository));
            if (equipmentDataRepository == null) throw new ArgumentNullException(nameof(equipmentDataRepository));

            _pointOfSaleRepository = pointOfSaleRepository;
            _equipmentDataRepository = equipmentDataRepository;
        }

        public async Task<IEnumerable<PointOfSaleDataDTO>> GetPointOfSaleData(int posId)
        {
            var posData = await _pointOfSaleRepository.GetPointOfSaleData(posId);
            var equipmentIds = await _equipmentDataRepository.GetPointOfSaleEquipment(posId);

            foreach (var pos in posData)
            {
                pos.EquipmentIds = equipmentIds;
            }

            return posData;
        }
    }
}
