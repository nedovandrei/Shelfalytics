using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private readonly int _emptyDistance = GlobalConstants.EmptyDistance;

        public StatisticsService(IEquipmentDataRepository equipmentDataRepository,
            IProductDataRepository productDataRepository)
        {
            if (equipmentDataRepository == null) throw new ArgumentNullException(nameof(equipmentDataRepository));
            if (productDataRepository == null) throw new ArgumentNullException(nameof(productDataRepository));

            _equipmentDataRepository = equipmentDataRepository;
            _productDataRepository = productDataRepository;
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

            var result = Math.Round(OOSCount / ZeroOOSCount * 100.0);

            return result;

        }
    }
}
