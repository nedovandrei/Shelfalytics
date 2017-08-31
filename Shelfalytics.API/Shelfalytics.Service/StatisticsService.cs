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
            var equipment = await _equipmentDataRepository.GetEquipmentById(equipmentId);
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

                        if (repeatingProductRows.Count() == 1 && distanceRead.Distance == equipment.EmptyDistance)
                        {
                            productOOSList.Add(product);
                            generalProductsOOS.Add(product);
                        }
                        else if (repeatingProductRows.Count() > 1)
                        {
                            if (
                                repeatingProductRows.All(
                                    x =>
                                        reading.DistanceReadings.All(y => y.Row == x.Row && y.Distance == equipment.EmptyDistance)))
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
                    ShortSKUName = g.First(x => x.ProductId == key).ShortSKUName,
                    ProductName = g.First(x => x.ProductId == key).ProductName,
                    OOSPercentage = Math.Round(g.Count(x => x.ProductId == key) / ZeroOOSCount * 100, 2)
                }
            ).ToList();
            detailedOOSData.TotalOOS = result;

            return detailedOOSData;

        }

        public async Task<EquipmentDetaildedOOSDTO> GetPOSOOS(int posId, GlobalFilter filter)
        {
            var pos = await _pointOfSaleRepository.GetPosEquipment(posId, filter);

            var result = new EquipmentDetaildedOOSDTO {OOSProducts = new List<EquipmentProductOOSDTO>()};
            foreach (var equipment in pos)
            {
                var equipmentData = await GetEquipmentOOS(equipment, filter);
                if (equipmentData.OOSProducts.Count > 0)
                {
                    foreach (var oosProduct in equipmentData.OOSProducts)
                    {
                        if (result.OOSProducts.Any(x => x.ProductName == oosProduct.ProductName))
                        {
                            result.OOSProducts.First(x => x.ProductName == oosProduct.ProductName).OOSPercentage +=
                                oosProduct.OOSPercentage;
                        }
                        else
                        {
                            result.OOSProducts.Add(oosProduct);
                        }
                    }
                }
                result.TotalOOS += equipmentData.TotalOOS;
            }

            return result;
        }

        public async Task<List<PointOfSaleOOSSummary>> GetPOSOOSSummary(GlobalFilter filter)
        {
            var posList = await _pointOfSaleRepository.GetPointsOfSales(filter.ClientId, filter.IsAdmin);
            var posOosSummary = new List<PointOfSaleOOSSummary>();

            foreach (var pos in posList)
            {
                var posOos = await GetPOSOOS(pos.PointOfSaleId, filter);
                posOosSummary.Add(new PointOfSaleOOSSummary
                {
                    PointOfSaleId = pos.PointOfSaleId,
                    PointOfSaleName = pos.PointOfSaleName,
                    OOSPercentage = posOos.TotalOOS,
                    Latitude = pos.Latitude,
                    Longitude = pos.Longitude,
                    PointOfSaleAddress = pos.PointOfSaleAddress
                });
            }

            return posOosSummary;
        }


        public async Task<IEnumerable<EquipmentProductSalesDTO>> GetProductSalesData(int equipmentId,
            GlobalFilter filter)
        {
            var res = await _saleRepository.GetEquipmentSales(equipmentId, filter);
            return res;
        }

        public async Task<EquipmentDetaildedOOSDTO> GetTopSkuOOS(GlobalFilter filter)
        {
            var posList = await _pointOfSaleRepository.GetPointsOfSales(filter.ClientId, filter.IsAdmin);
            var result = new EquipmentDetaildedOOSDTO {OOSProducts = new List<EquipmentProductOOSDTO>()};

            foreach (var pos in posList)
            {
                var posOos = await GetPOSOOS(pos.PointOfSaleId, filter);

                if (posOos.OOSProducts.Count > 0)
                {
                    foreach (var posOosProduct in posOos.OOSProducts)
                    {
                        if (result.OOSProducts.Any(x => x.ProductName == posOosProduct.ProductName))
                        {
                            result.OOSProducts.First(x => x.ProductName == posOosProduct.ProductName).OOSPercentage +=
                                posOosProduct.OOSPercentage;
                        }
                        else
                        {
                            result.OOSProducts.Add(posOosProduct);
                        }
                    }
                }

                result.TotalOOS += posOos.TotalOOS;
            }

            return result;
        }

        public async Task<SalesSummaryDTO> GetProductSalesSummary(GlobalFilter filter)
        {
            var productSalesSummary = await _saleRepository.GetProductSalesSummary(filter);
            var saleSummary = new SalesSummaryDTO()
            {
                Products = productSalesSummary,
                SalesCount = productSalesSummary.Sum(x => x.Sales)
            };

            return saleSummary;
        }

        public async Task<IEnumerable<EquipmentLossesDueToOOSDTO>> GetEquipmentLossesDueToOos(int equipmentId, GlobalFilter filter)
        {
            var equipment = await _equipmentDataRepository.GetEquipmentById(equipmentId);
            var readings = await _equipmentDataRepository.GetFilteredEquipmentData(equipmentId, filter);
            var planogram = await _productDataRepository.GetEquipmentPlanogram(equipmentId);

            var readingsList = readings.OrderBy(x => x.TimeStamp).ToList();

            var lossesList = new List<EquipmentLossesDueToOOSDTO>();

            for (var columnIndex = 0; columnIndex < planogram.Count(); columnIndex++)
            {
                for (var rowIndex = 0; rowIndex < readingsList.Count(); rowIndex++)
                {
                    var rowSkip = 0;

                    var distanceList = readingsList[rowIndex].DistanceReadings.ToList();
                    if (distanceList[columnIndex].Distance == equipment.EmptyDistance)
                    {
                        var oosStartPoint = readingsList[rowIndex].TimeStamp;
                        var oosEndPoint = new DateTime();
                        var isOngoing = false;
                        var product = planogram.First(x => x.Row == columnIndex + 1);
                        
                        // finding out the timespan for OOS for a row
                        for (var endPointIndex = 1; endPointIndex <= readingsList.Count - rowIndex; endPointIndex++)
                        {
                            if (rowIndex + endPointIndex >= readingsList.Count)
                            {
                                oosEndPoint = readingsList[rowIndex + endPointIndex - 1].TimeStamp;
                                isOngoing = true;
                            }
                            else
                            {
                                var nextDistanceRead = readingsList[rowIndex + endPointIndex].DistanceReadings.ToList();
                                if (nextDistanceRead[columnIndex].Distance == equipment.EmptyDistance)
                                {
                                    rowSkip++;
                                    if (endPointIndex == readingsList.Count - rowIndex - 1)
                                    {
                                        oosEndPoint = readingsList[rowIndex + endPointIndex].TimeStamp;
                                        isOngoing = true;
                                    }
                                }
                                else
                                {
                                    oosEndPoint = readingsList[rowIndex + endPointIndex].TimeStamp;
                                    break;
                                }
                            }
                            
                        }

                        var salesAverage = await _saleRepository.GetProductSalesAverage(product.ProductId, new GlobalFilter
                        {
                            StartTime = filter.StartTime.Subtract(filter.EndTime - filter.StartTime),
                            EndTime = filter.EndTime.Subtract(filter.EndTime - filter.StartTime)
                        });

                        var timeSpan = oosEndPoint - oosStartPoint;
                        decimal targetDaysCount;
                        if (timeSpan.Days == 0)
                        {
                            if (timeSpan.Hours == 0)
                            {
                                targetDaysCount = timeSpan.Minutes / 3600.0m;
                            }
                            else
                            {
                                targetDaysCount = timeSpan.Hours / 24.0m;
                            }
                        }
                        else
                        {
                            targetDaysCount = timeSpan.Days + (timeSpan.Hours / 24.0m) + (timeSpan.Minutes / 3600.0m);
                        }

                        var losses = new EquipmentLossesDueToOOSDTO
                        {
                            SKUName = product.SKUName,
                            ShortSKUName = product.ShortSKUName,
                            OOSStart = oosStartPoint,
                            OOSEnd = oosEndPoint,
                            TimePeriod = timeSpan,
                            TimePeriodDays = timeSpan.Days,
                            TimePeriodHours = timeSpan.Hours,
                            TimePeriodMinutes = timeSpan.Minutes,
                            TimePeriodSeconds = timeSpan.Seconds,
                            Losses = (salesAverage.Any() ? salesAverage.FirstOrDefault().AverageSales : 0.00m) * targetDaysCount * product.Price,
                            AverageSales = salesAverage.Any() ? salesAverage.FirstOrDefault().AverageSales : 0.00m,
                            isOngoing = isOngoing
                        };
                        lossesList.Add(losses);

                        rowIndex += rowSkip;
                    }
                }
            }

            return lossesList;
        }

        public async Task<LossesDueToOOSSummaryDTO> GetLossesDueToOOSSummary(GlobalFilter filter)
        {
            var equipments = await _equipmentDataRepository.GetEquipments(filter);

            var lossesList = new List<EquipmentLossesDueToOOSDTO>();

            foreach (var equipment in equipments)
            {
                var equipmentLosses = await GetEquipmentLossesDueToOos(equipment.Id, filter);
                lossesList.AddRange(equipmentLosses);
            }

            var targetList = lossesList.GroupBy(x => new {x.SKUName, x.ShortSKUName}).Select(x => new EquipmentLossesDueToOOSDTO
            {
                SKUName = x.Key.SKUName,
                ShortSKUName = x.Key.ShortSKUName,
                AverageSales =
                    x.Where(y => y.SKUName == x.Key.SKUName).Sum(y => y.AverageSales) /
                    x.Count(y => y.SKUName == x.Key.SKUName),
                Losses = x.Where(y => y.SKUName == x.Key.SKUName).Sum(y => y.Losses),
                //TimePeriod = x.Where(y => y.SKUName == x.Key.SKUName).Sum(y => y.TimePeriod)
                TimePeriodDays = x.Where(y => y.SKUName == x.Key.SKUName).Sum(y => y.TimePeriodDays),
                TimePeriodHours = x.Where(y => y.SKUName == x.Key.SKUName).Sum(y => y.TimePeriodHours),
                TimePeriodMinutes = x.Where(y => y.SKUName == x.Key.SKUName).Sum(y => y.TimePeriodMinutes),
                TimePeriodSeconds = x.Where(y => y.SKUName == x.Key.SKUName).Sum(y => y.TimePeriodSeconds),
            });

            var result = new LossesDueToOOSSummaryDTO
            {
                Total = targetList.Sum(x => x.Losses),
                LossesByProducts = targetList
            };

            return result;
        }
    }
}
