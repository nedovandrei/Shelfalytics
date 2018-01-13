using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Shelfalytics.API.Models;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.ServiceInterface;
using System.Web.Hosting;
using Shelfalytics.API.Models.SShelfModels;
using Shelfalytics.RepositoryInterface.DTO.SShelfIntegration;
using Shelfalytics.Model.DbModels.SShelfIntegration;

namespace Shelfalytics.API.Controllers
{
    
    [RoutePrefix("api/EquipmentData")]
    public class EquipmentDataController : ApiController
    {
        private readonly IEquipmentDataService _equipmentDataService;
        private readonly ISShelfService _sShelfService;
        
        public EquipmentDataController(IEquipmentDataService equipmentDataService, ISShelfService sShelfService)
        {
            if (sShelfService == null)
            {
                throw new ArgumentNullException(nameof(sShelfService));
            }

            _equipmentDataService = equipmentDataService;
            _sShelfService = sShelfService;
        }

        [HttpGet]
        [Authorize]
        public async Task<HttpResponseMessage> GetLatestEquipmentData(int equipmentId)
        {
            var response = await _equipmentDataService.GetLatestEquipmentData(equipmentId);
            return Request.CreateResponse(response);
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage TestEquipmentReadingSave(EquipmentReadingTest obj)
        {
            return Request.CreateResponse(obj.Message);
        }

        [Route("register")]
        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> EquipmentReadingSave(EquipmentReadingModel model)
        {
            var dataToSend = new EquipmentReadingDTO()
            {
                IMEI = model.IMEI,
                Temperature = Math.Round(model.Temperature),
                DistanceSensors = model.DistanceSensors.Select(x => new EquipmentDistanceReadingDTO
                {
                    Row = x.Row,
                    Distance = x.Distance
                }),
                IsPoweredOn = model.IsPoweredOn
            };

            await _equipmentDataService.RegisterReading(dataToSend);


            return Request.CreateResponse(HttpStatusCode.OK);
            
        }

        [Route("open")]
        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> EquipmentDoorOpened(EquipmentReadingModel model)
        {
            var dataToSend = new EquipmentReadingDTO()
            {
                IMEI = model.IMEI,
                //Temperature = model.Temperature,
                //DistanceSensors = model.DistanceSensors.Select(x => new EquipmentDistanceReadingDTO
                //{
                //    Row = x.Row,
                //    Distance = x.Distance
                //}),
                //IsPoweredOn = model.IsPoweredOn
            };

            await _equipmentDataService.RegisterDoorOpen(dataToSend);


            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #region Integration endpoints

        // Register for SShelf data
        [Route("importData")]
        public async Task<HttpResponseMessage> SShelfEquipmentReadingSave(SShelfEquipmentReadingModel model)
        {
            var pushers = new List<SShelfEquipmentPusherReading>();
            var sales = new List<SShelfEquipmentSalesReading>();

            foreach (var pusher in model.Pushers)
            {
                pushers.Add(new SShelfEquipmentPusherReading()
                {
                    PusherId = pusher.Id,
                    Percentage = pusher.Percent,
                    Status = pusher.Status,
                    Balance = pusher.Balance,
                    Error = pusher.Error
                });
            }

            foreach (var mark in model.Marks)
            {
                sales.Add( new SShelfEquipmentSalesReading()
                {
                    ProductId = mark.Id,
                    SalesCount = mark.Delta
                });
            }

            var modelDto = new SShelfEquipmentReadingDTO()
            {
                ModemId = model.Modem,
                Power = model.Power == "Y",
                Signal = model.Signal,
                GpsLongitude = model.Gps_long,
                GpsLatitude = model.Gps_lat,
                BatteryLevel = model.Bat,
                GsmLongitude = model.Gsm_long,
                GsmLatitude = model.Gsm_lat,
                Pushers = pushers,
                Marks = sales
            };

            await _sShelfService.RegisterReading(modelDto);

            //await _sShelfService.Test();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("testApi")]
        public async Task<HttpResponseMessage> Test()
        {
            await _sShelfService.Test();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion
    }
}
