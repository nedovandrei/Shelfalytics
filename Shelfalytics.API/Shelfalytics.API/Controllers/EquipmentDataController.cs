using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Shelfalytics.API.Models;
using Shelfalytics.RepositoryInterface.Repositories;
using Shelfalytics.ServiceInterface;

namespace Shelfalytics.API.Controllers
{
    public class EquipmentDataController : ApiController
    {
        private readonly IEquipmentDataService _equipmentDataService;

        
        public EquipmentDataController(IEquipmentDataService equipmentDataService)
        {
            if (equipmentDataService == null)
            {
                throw new ArgumentNullException(nameof(equipmentDataService));
            }
            _equipmentDataService = equipmentDataService;
        }

        [HttpGet]
        
        public async Task<HttpResponseMessage> GetLatestEquipmentData(int equipmentId)
        {
            var response = await _equipmentDataService.GetLatestEquipmentData(equipmentId);
            return Request.CreateResponse(response);
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage TestEquipmentReadingSave(EquipmentReadingModel obj)
        {
            return Request.CreateResponse(obj.Message);
        }
    }
}
