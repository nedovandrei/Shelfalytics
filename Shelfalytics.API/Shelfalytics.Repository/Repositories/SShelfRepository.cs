using Newtonsoft.Json;
using Shelfalytics.RepositoryInterface.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Shelfalytics.Model.DbModels.SShelfIntegration;
using Shelfalytics.RepositoryInterface;
using Shelfalytics.RepositoryInterface.DTO.SShelfIntegration;
using Shelfalytics.RepositoryInterface.DTO.SShelfIntegration.API;

namespace Shelfalytics.Repository.Repositories
{
    public class SShelfRepository : ISShelfRepository
    {
        private const string EndPointPrefix = "http://smart-chillers.tk/api/";
        private const string AuthEndPoint = "auth/?";
        private const string StoresEndPoint = "getStores?";
        private const string UnitsEndPoint = "getUnits?";
        private const string UnitEndPoint = "getUnit?";
        private const string BrandsEndPoint = "getBrands?";
        private const string AuthUserParam = "user=";
        private const string AuthPassParam = "pass=";
        private const string AuthTokenParam = "token=";
        private const string IdParam = "id=";
        private const string UserName = "apiuser";
        private const string Password = "api321pass";
        private string Token;
        private readonly HttpClient _client = new HttpClient();
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public SShelfRepository(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null) throw new ArgumentNullException(nameof(unitOfWorkFactory));
            _unitOfWorkFactory = unitOfWorkFactory;
            Authorize();
            
        }

        public async Task Authorize()
        {
            var responseString = await _client.GetStringAsync(EndPointPrefix + AuthEndPoint + AuthUserParam + UserName + "&" + AuthPassParam + Password);
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

        public async Task<IEnumerable<SShelfEquipmentReadingDTO>> GetLatestReading(string modemId)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsIdentityDbContext())
            {
                var query = from reading in uow.Set<SShelfEquipmentReading>()
                    join pusher in uow.Set<SShelfEquipmentPusherReading>() on reading.Id equals
                    pusher.EquipmentReadingId into pusherList
                    //join sale in uow.Set<SShelfEquipmentSalesReading>() on reading.Id equals sale.EquipmentReadingId into saleList
                    where reading.ModemId == modemId
                    select new SShelfEquipmentReadingDTO()
                    {
                        GpsLatitude = reading.GpsLatitude,
                        Power = reading.Power,
                        GpsLongitude = reading.GpsLongitude,
                        GsmLatitude = reading.GsmLatitude,
                        GsmLongitude = reading.GsmLongitude,
                        ModemId = reading.ModemId,
                        Signal = reading.Signal,
                        BatteryLevel = reading.BatteryLevel,
                        Pushers = pusherList
                    };

                return await query.ToListAsync();

            }
        }

        #region SShelf API Calls

        public async Task<IEnumerable<SShelfStoreDTO>> GetSShelfStores()
        {

            var endPoint = EndPointPrefix + StoresEndPoint + AuthTokenParam;
            var response = await GetRequestHelper<SShelfStoresResponse>(endPoint);
            var resultList = response.Shops.ToList();

            return resultList;
        }

        public async Task<IEnumerable<SShelfEquipmentDTO>> GetSShelfEquipment()
        {
            
            var endPoint = EndPointPrefix + UnitsEndPoint + AuthTokenParam;
            var response = await GetRequestHelper<SShelfEquipmentResponse>(endPoint);
            var resultList = response.Units.ToList();
                
            return resultList;
        }

        public async Task<IEnumerable<SShelfBrandDTO>> GetSShelfProducts()
        {
            var endPoint = EndPointPrefix + BrandsEndPoint + AuthTokenParam;
            var response = await GetRequestHelper<SShelfBrandsResponse>(endPoint);
            var resultList = response.Brands.ToList();

            return resultList;
        }

        public async Task<SShelfEquipmentDTO> GetSShelfEquipmentUnit(int id)
        {
            var endPoint = EndPointPrefix + UnitEndPoint + IdParam + id + "&" + AuthTokenParam;
            var response = await GetRequestHelper<SShelfUnitResponse>(endPoint);
            var resultList = response.Unit;

            return resultList;
        }

        #endregion

        #region Base Helpers
        private async Task<TEntity> GetRequestHelper<TEntity>(string endPoint) where TEntity : SShelfResponse
        {
            bool errorFlag;
            var counter = 0;
            var errorLog = "";

            do
            {
                errorFlag = false;

                var responseString = await _client.GetStringAsync(endPoint + Token);

                var result = JsonConvert.DeserializeObject<TEntity>(responseString);
                if (result.Error)
                {
                    if (result.Message == "Authorization fail")
                    {
                        await Authorize();
                    }
                    errorLog += result.Message + "\n";
                    errorFlag = true;
                }
                else if (!result.Error)
                {
                    return result;
                }
                counter++;
            } while (errorFlag && counter <= 10);

            throw new Exception("Error loading shops:\n" + errorLog);
            
        }
        #endregion
    }

    #region GET Requests Response Models

    internal abstract class SShelfResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
    }

    internal class SShelfAuthResponse : SShelfResponse
    {
        public string Token { get; set; }
    }

    internal class SShelfStoresResponse : SShelfResponse
    {
        public IEnumerable<SShelfStoreDTO> Shops { get; set; }
    }

    internal class SShelfEquipmentResponse : SShelfResponse
    {
        public IEnumerable<SShelfEquipmentDTO> Units { get; set; }
    }

    internal class SShelfBrandsResponse : SShelfResponse
    {
        public IEnumerable<SShelfBrandDTO> Brands { get; set; }
    }

    internal class SShelfUnitResponse : SShelfResponse
    {
        public SShelfEquipmentDTO Unit { get; set; }
    }
    #endregion
}
