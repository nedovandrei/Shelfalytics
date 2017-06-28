using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shelfalytics.RepositoryInterface.Repositories;
using Shelfalytics.ServiceInterface;
using Shelfalytics.ServiceInterface.ViewModels;

namespace Shelfalytics.Service
{
    public class EquipmentDataService : IEquipmentDataService
    {
        //predefined
        private readonly int _emptyDistance = 85;
        private readonly int _fullStockDistance = 2;
        private readonly int _oneBottleLength = 7;

        private readonly IEquipmentDataRepository _equipmentDataRepository;
        private readonly IProductDataRepository _productDataRepository;

        public EquipmentDataService(IEquipmentDataRepository equipmentDataRepository,
            IProductDataRepository productDataRepository)
        {
            if (equipmentDataRepository == null) throw new ArgumentNullException(nameof(equipmentDataRepository));
            if (productDataRepository == null) throw new ArgumentNullException(nameof(productDataRepository));

            _equipmentDataRepository = equipmentDataRepository;
            _productDataRepository = productDataRepository;
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
                        Percentage = 100 - ((double)(sensorReading.Distance - _fullStockDistance) / (double)(_emptyDistance - _fullStockDistance) * 100),
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
                    EquipmentType = equipmentData.EquipmentType,
                    ModelName = equipmentData.ModelName,
                    Temperature = equipmentData.Temperature,
                    OpenCloseCountToday = equipmentData.OpenCloseCountToday,
                    RowCount = equipmentData.RowCount,
                    RowInfo = rowViewModelList,
                    TimeStamp = equipmentData.TimeStamp
                };
                viewModelList.Add(viewModel);
            }
            return viewModelList;
        }
    }
}
