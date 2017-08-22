using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shelfalytics.Model.DbModels;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Repositories;
using Shelfalytics.ServiceInterface;
using Shelfalytics.ServiceInterface.Constants;
using Shelfalytics.ServiceInterface.ViewModels;

namespace Shelfalytics.Service
{
    public class EquipmentDataService : IEquipmentDataService
    {
        //predefined
        private readonly int _emptyDistance = GlobalConstants.EmptyDistance;
        private readonly int _fullStockDistance = GlobalConstants.FullStockDistance;
        private readonly int _oneBottleLength = GlobalConstants.OneBottleLength;

        private readonly IEquipmentDataRepository _equipmentDataRepository;
        private readonly IProductDataRepository _productDataRepository;
        private readonly ISaleRepository _saleRepository;

        public EquipmentDataService(IEquipmentDataRepository equipmentDataRepository,
            IProductDataRepository productDataRepository, ISaleRepository saleRepository)
        {
            if (equipmentDataRepository == null) throw new ArgumentNullException(nameof(equipmentDataRepository));
            if (productDataRepository == null) throw new ArgumentNullException(nameof(productDataRepository));

            _equipmentDataRepository = equipmentDataRepository;
            _productDataRepository = productDataRepository;
            _saleRepository = saleRepository;
        }

        public async Task<IEnumerable<EquipmentReadingsViewModel>> GetLatestEquipmentData(int equipmentId)
        {
            var rawEquipmentData = await _equipmentDataRepository.GetLatestEquipmentData(equipmentId);
            var planogramData = await _productDataRepository.GetEquipmentPlanogram(equipmentId);

            var viewModelList = new List<EquipmentReadingsViewModel>();

            foreach (var equipmentData in rawEquipmentData)
            {
                var rowViewModelList = new List<EquipmentRowInfoViewModel>();
                foreach (var sensorReading in equipmentData.DistanceReadings)
                {
                    var productData = planogramData.FirstOrDefault(x => x.Row == sensorReading.Row);
                    if (productData == null)
                    {
                        continue;
                    }
                    var rowViewModel = new EquipmentRowInfoViewModel
                    {
                        Row = sensorReading.Row,
                        Percentage = 100 - ((double)(sensorReading.Distance - equipmentData.FullDistance) / (double)(equipmentData.EmptyDistance - equipmentData.FullDistance) * 100),
                        ProductName = productData.ProductName,
                        SKUName = productData.SKUName
                    };
                    rowViewModelList.Add(rowViewModel);
                }
                var viewModel = new EquipmentReadingsViewModel
                {
                    Id = equipmentData.Id,
                    ClientName = equipmentData.ClientName,
                    PointOfSaleName = equipmentData.PointOfSaleName,
                    PointOfSaleAddress = equipmentData.PointOfSaleAddress,
                    PointOfSaleTelephone = equipmentData.PointOfSaleTelephone,
                    EquipmentType = equipmentData.EquipmentType,
                    ModelName = equipmentData.ModelName,
                    Temperature = equipmentData.Temperature,
                    OpenCloseCountToday = equipmentData.OpenCloseCountToday,
                    RowCount = equipmentData.RowCount,
                    RowInfo = rowViewModelList,
                    TimeStamp = equipmentData.TimeStamp,
                    Width = equipmentData.Width,
                    YCount = equipmentData.YCount
                };
                viewModelList.Add(viewModel);
            }
            return viewModelList;
        }

        public async Task RegisterReading(EquipmentReadingDTO reading)
        {
            var equipment = await _equipmentDataRepository.GetEquipmentByIMEI(reading.IMEI);
            var equipmentHasReadings = await _equipmentDataRepository.EquipmentHasReadings(equipment.Id);


            var readingModel = new EquipmentReading
            {
                EquipmentId = equipment.Id,
                Temperature = reading.Temperature,
                TimeSpamp = DateTime.Now,
                WasOpened = true
            };

            var planogramData = await _productDataRepository.GetEquipmentPlanogram(equipment.Id);


            var previousReading = new EquipmentReadingGetDTO();
            if (equipmentHasReadings)
            {
                 previousReading = await _equipmentDataRepository.GetLatestReading(equipment.Id);
            }
            

            var registeredReading = await _equipmentDataRepository.RegisterEquipmentReading(readingModel);

            var distanceReadingsList = new List<EquipmentDistanceReading>();
            for(var i = 0; i < reading.DistanceSensors.Count(); i++)
            {
                // sales registration logic
                if (equipmentHasReadings && i != previousReading.SensorReadings.Count())
                {
                    //getting planogram data with bottle diameters
                    var product = planogramData.First(x => x.Row == reading.DistanceSensors.ToList()[i].Row);

                    // calculate difference between new reading data and previous
                    var delta = previousReading.SensorReadings.ToList()[i].Distance - reading.DistanceSensors.ToList()[i].Distance;

                    // if difference is higher than 3/4 of predefined one bottle length;
                    // just a precaution, since the code below will result 0 in any case that
                    // Delta is below _oneBottleLength
                    if (delta < -(product.BottleDiameter / 1.5))
                    {
                        delta *= -1;
                        var salesQtyUnrounded = Math.Round(delta / product.BottleDiameter, 1);

                        // the whole point of the code below is to shift the Round Logic
                        // so that the midpoint of Round is 0.7.
                        // Anything below 0.7 will be floored, anything above - ceiled
                        var salesQty = salesQtyUnrounded % Math.Floor(salesQtyUnrounded) >= 0.7 ?
                            (int)Math.Round(salesQtyUnrounded) : (int)Math.Floor(salesQtyUnrounded);
                        if (salesQty != 0)
                        {
                            //var product = planogramData.First(x => x.Row == reading.DistanceSensors.ToList()[i].Row);
                            var saleRecord = new Sale
                            {
                                EquipmentId = equipment.Id,
                                ProductId = product.ProductId,
                                Quantity = salesQty,
                                TimeStamp = DateTime.Now
                            };
                            await _saleRepository.RegisterSale(saleRecord);
                        }
                    }
                    
                }
                
                var distRead = new EquipmentDistanceReading
                {
                    EquipmentReadingId = registeredReading.Id,
                    Row = reading.DistanceSensors.ToList()[i].Row,
                    Distance = reading.DistanceSensors.ToList()[i].Distance
                };

                distanceReadingsList.Add(distRead);
            }

            await _equipmentDataRepository.RegisterEquipmentDistanceReadings(distanceReadingsList);

        }
    }
}
