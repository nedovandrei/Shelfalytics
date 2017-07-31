using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Helpers;
using Shelfalytics.RepositoryInterface.Repositories;
using Shelfalytics.ServiceInterface;
using Shelfalytics.ServiceInterface.Constants;

namespace Shelfalytics.Service
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IEquipmentDataRepository _equipmentDataRepository;
        private readonly IProductDataRepository _productDataRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IPointOfSaleRepository _pointOfSaleRepository;

        private readonly int _emptyDistance = GlobalConstants.EmptyDistance;

        public StatisticsService(IEquipmentDataRepository equipmentDataRepository,
            IProductDataRepository productDataRepository, ISaleRepository saleRepository, IPointOfSaleRepository pointOfSaleRepository)
        {
            if (equipmentDataRepository == null) throw new ArgumentNullException(nameof(equipmentDataRepository));
            if (productDataRepository == null) throw new ArgumentNullException(nameof(productDataRepository));

            _equipmentDataRepository = equipmentDataRepository;
            _productDataRepository = productDataRepository;
            _saleRepository = saleRepository;
            _pointOfSaleRepository = pointOfSaleRepository;
        }

        public async Task<double> GetEquipmentOOS(int equipmentId, GlobalFilter filter)
        {
            var readings = await _equipmentDataRepository.GetFilteredEquipmentData(equipmentId, filter);

            var ZeroOOSCount = 0.0;
            var OOSCount = 0.0;

            foreach (var reading in readings)
            {
                ZeroOOSCount += reading.DistanceReadings.Count();
                OOSCount += reading.DistanceReadings.Count(x => x.Distance == _emptyDistance);
            }

            if (ZeroOOSCount == 0.0)
            {
                return 0;
            }

            var result = Math.Round(OOSCount / ZeroOOSCount * 100.0, 2);

            return result;

        }

        public async Task<double> GetPOSOOS(int posId, GlobalFilter filter)
        {
            var pos = await _pointOfSaleRepository.GetPosEquipment(posId);

            var result = 0.0;
            foreach (var equipment in pos)
            {
                result += await GetEquipmentOOS(equipment, filter);
            }

            return Math.Round(result / pos.Count(), 2);
        }

        public async Task<IEnumerable<EquipmentProductSalesDTO>> GetProductSalesData(int equipmentId,
            GlobalFilter filter)
        {
            var res = await _saleRepository.GetEquipmentSales(equipmentId, filter);
            return res;
        }
    }
}
