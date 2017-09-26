using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Shelfalytics.API.Models;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.ServiceInterface;
using System.Web.Hosting;

namespace Shelfalytics.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/EquipmentData")]
    public class EquipmentDataController : ApiController
    {
        private readonly IEquipmentDataService _equipmentDataService;

        
        public EquipmentDataController(IEquipmentDataService equipmentDataService)
        {
            _equipmentDataService = equipmentDataService;
        }

        [HttpGet]
        
        public async Task<HttpResponseMessage> GetLatestEquipmentData(int equipmentId)
        {
            var response = await _equipmentDataService.GetLatestEquipmentData(equipmentId);
            return Request.CreateResponse(response);
        }

        [HttpPost]
        public HttpResponseMessage TestEquipmentReadingSave(EquipmentReadingTest obj)
        {
            return Request.CreateResponse(obj.Message);
        }

        [Route("register")]
        [HttpPost]
        public async Task<HttpResponseMessage> EquipmentReadingSave(EquipmentReadingModel model)
        {
            var dataToSend = new EquipmentReadingDTO()
            {
                IMEI = model.IMEI,
                Temperature = model.Temperature,
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
    }
}
