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

        public async Task<EquipmentDetaildedOOSDTO> GetEquipmentOOS(int equipmentId, GlobalFilter filter)
        {
            var readings = await _equipmentDataRepository.GetFilteredEquipmentData(equipmentId, filter);
            var planogram = await _productDataRepository.GetEquipmentPlanogram(equipmentId);


            var ZeroOOSCount = 0.0;
            var OOSCount = 0.0;
            var generalProductsOOS = new List<EquipmentPlanogramDTO>();
            var detailedOOSData = new EquipmentDetaildedOOSDTO();

            foreach (var reading in readings)
            {
                ZeroOOSCount += reading.DistanceReadings.Count();
                var productOOSList = new List<EquipmentPlanogramDTO>();

                foreach (var distanceRead in reading.DistanceReadings)
                {
                    if (planogram.Any(x => x.Row == distanceRead.Row))
                    {
                        var product = planogram.FirstOrDefault(x => x.Row == distanceRead.Row);
                        var repeatingProductRows = planogram.Where(x => x.ProductId == product.ProductId);

                        if (repeatingProductRows.Count() == 1 && distanceRead.Distance == _emptyDistance)
                        {
                            productOOSList.Add(product);
                            generalProductsOOS.Add(product);
                        }
                        else if (repeatingProductRows.Count() > 1)
                        {
                            if (
                                repeatingProductRows.All(
                                    x =>
                                        reading.DistanceReadings.All(y => y.Row == x.Row && y.Distance == _emptyDistance)))
                            {
                                if (productOOSList.All(x => x != product))
                                {
                                    productOOSList.Add(product);
                                    generalProductsOOS.Add(product);
                                }
                            }
                        }
                    }
                }

                OOSCount += productOOSList.Count();
            }

            if (ZeroOOSCount == 0.0)
            {
                return new EquipmentDetaildedOOSDTO()
                {
                    TotalOOS = 0,
                    OOSProducts = new List<EquipmentProductOOSDTO>()
                };
            }

            var result = Math.Round(OOSCount / ZeroOOSCount * 100.0, 2);

            detailedOOSData.OOSProducts = generalProductsOOS.GroupBy
            (
                x => x.ProductId,
                (key, g) => new EquipmentProductOOSDTO
                {
                    ProductId = key,
                    Row = g.First(x => x.ProductId == key).Row,
                    EquipmentId = g.First(x => x.ProductId == key).EquipmentId,
                    SKUName = g.First(x => x.ProductId == key).SKUName,
                    ProductName = g.First(x => x.ProductId == key).ProductName,
                    OOSPercentage = Math.Round(g.Count(x => x.ProductId == key) / ZeroOOSCount * 100, 2)
                }
            ).ToList();
            detailedOOSData.TotalOOS = result;

            return detailedOOSData;

        }

        public async Task<EquipmentDetaildedOOSDTO> GetPOSOOS(int posId, GlobalFilter filter)
        {
            var pos = await _pointOfSaleRepository.GetPosEquipment(posId);

            var result = new EquipmentDetaildedOOSDTO();
            result.OOSProducts = new List<EquipmentProductOOSDTO>();
            foreach (var equipment in pos)
            {
                var equipmentData = await GetEquipmentOOS(equipment, filter);
                if (equipmentData.OOSProducts.Count > 0)
                {
                    result.OOSProducts.AddRange(equipmentData.OOSProducts);
                }
                result.TotalOOS += equipmentData.TotalOOS;
            }

            return result;
        }

        public async Task<IEnumerable<EquipmentProductSalesDTO>> GetProductSalesData(int equipmentId,
            GlobalFilter filter)
        {
            var res = await _saleRepository.GetEquipmentSales(equipmentId, filter);
            return res;
        }
    }
}
