using Shelfalytics.Model.DbModels;
using Shelfalytics.RepositoryInterface.Helpers;
using Shelfalytics.RepositoryInterface.Repositories;
using Shelfalytics.ServiceInterface;
using System;
using System.Threading.Tasks;

namespace Shelfalytics.Service
{
    public class TestService : ITestService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IEquipmentDataRepository _equipmentDataRepository;
        private readonly IProductDataRepository _productDataRepository;

        public TestService(ISaleRepository saleRepository, IEquipmentDataRepository equipmentDataRepository, IProductDataRepository productDataRepository)
        {
            if (saleRepository == null)
            {
                throw new ArgumentNullException(nameof(saleRepository));
            }

            if (equipmentDataRepository == null)
            {
                throw new ArgumentNullException(nameof(equipmentDataRepository));
            }

            if (productDataRepository == null)
            {
                throw new ArgumentNullException(nameof(productDataRepository));
            }

            _saleRepository = saleRepository;
            _equipmentDataRepository = equipmentDataRepository;
            _productDataRepository = productDataRepository;
        }

        public async Task FillEquipmentWithFakeSaleData(int equipmentId, GlobalFilter timeSpan)
        {
            //var saleRecord = new Sale
            //{
            //    EquipmentId = equipment.Id,
            //    ProductId = product.ProductId,
            //    Quantity = salesQty,
            //    TimeStamp = DateTime.Now
            //};
            //await _saleRepository.RegisterSale(saleRecord);

            var dateSpanRandomizer = new Random();
            var salesQtyRandomizer = new Random();


            var planogram = await _productDataRepository.GetEquipmentPlanogram(equipmentId);

            foreach (var product in planogram)
            {
                var dateSpan = timeSpan.StartTime;
                
                while (dateSpan < timeSpan.EndTime)
                {
                    var randomMinutes = dateSpanRandomizer.Next(5, 50);
                    var randomQty = salesQtyRandomizer.Next(1, 4);
                    await _saleRepository.RegisterSale(new Sale
                    {
                        EquipmentId = equipmentId,
                        ProductId = product.ProductId,
                        Quantity = randomQty,
                        TimeStamp = dateSpan
                    });
                    dateSpan = dateSpan.AddMinutes(randomMinutes);
                }
            }

           
            //try
            //{

            //}
            //catch(Exception ex)
            //{
            //    return 
            //}
        }
    }
}
