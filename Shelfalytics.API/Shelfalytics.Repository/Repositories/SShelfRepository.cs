using Newtonsoft.Json;
using Shelfalytics.RepositoryInterface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Shelfalytics.Model.DbModels.SShelfIntegration;
using Shelfalytics.RepositoryInterface;
using Shelfalytics.RepositoryInterface.DTO.SShelfIntegration;

namespace Shelfalytics.Repository.Repositories
{
    public class SShelfRepository : ISShelfRepository
    {
        private readonly string EndPoint = "http://smart-chillers.tk/api/auth/?";
        private readonly string Param1 = "user=";
        private readonly string Param2 = "pass=";
        private readonly string UserName = "apiuser";
        private readonly string Password = "api321pass";
        private string Token;
        private readonly HttpClient client = new HttpClient();
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public SShelfRepository(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null) throw new ArgumentNullException(nameof(unitOfWorkFactory));
            _unitOfWorkFactory = unitOfWorkFactory;
            Authorize();
            
        }

        public async Task Authorize()
        {
            var responseString = await client.GetStringAsync(EndPoint + Param1 + UserName + "&" + Param2 + Password);
            //JsonConvert.DeserializeAnonymousType(responseString, data);
            var data = JsonConvert.DeserializeObject<SShelfAuthResponse>(responseString);
            if (!data.Error)
            {
                Token = data.Token;
            }
            else
            {
                throw new Exception("error authenticating");
            }
        }

        public async Task RegisterReading(SShelfEquipmentReadingDTO reading)
        {
            using (var uow = _unitOfWorkFactory.GetSShelfIntegrationDbContext())
            {
                var dbReading = new SShelfEquipmentReading()
                {
                    ModemId = reading.ModemId,
                    Power = reading.Power,
                    Signal = reading.Signal,
                    BatteryLevel = reading.BatteryLevel,
                    GpsLatitude = reading.GpsLatitude,
                    GpsLongitude = reading.GpsLongitude,
                    GsmLatitude = reading.GsmLatitude,
                    GsmLongitude = reading.GsmLongitude,
                    TimeStamp = DateTime.Now
                };

                uow.Add(dbReading);
                await uow.CommitAsync();

                var pusherReadings = new List<SShelfEquipmentPusherReading>();
                foreach (var pusher in reading.Pushers)
                {
                    pusherReadings.Add(new SShelfEquipmentPusherReading()
                    {
                        PusherId = pusher.PusherId,
                        EquipmentReadingId = dbReading.Id,
                        Balance = pusher.Balance,
                        Percentage = pusher.Percentage,
                        Status = pusher.Status,
                        Error = pusher.Error
                    });
                }

                // TODO: ADD SALES REGISTERING FOR Shelfalytics.Sales

                var sales = new List<SShelfEquipmentSalesReading>();
                foreach (var mark in reading.Marks)
                {
                    sales.Add(new SShelfEquipmentSalesReading()
                    {
                        EquipmentReadingId = dbReading.Id,
                        ProductId = mark.ProductId,
                        SalesCount = mark.SalesCount
                    });
                }

                uow.AddRange(pusherReadings);
                uow.AddRange(sales);
                await uow.CommitAsync();
            }
        }
    }

    internal class SShelfAuthResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
