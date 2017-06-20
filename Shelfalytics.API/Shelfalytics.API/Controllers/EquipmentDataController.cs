using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Shelfalytics.RepositoryInterface.Repositories;

namespace Shelfalytics.API.Controllers
{
    public class EquipmentDataController : ApiController
    {
        private readonly IEquipmentDataRepository _equipmentDataRepository;

        public EquipmentDataController(IEquipmentDataRepository equipmentDataRepository)
        {
            if (equipmentDataRepository == null)
            {
                throw new ArgumentNullException(nameof(equipmentDataRepository));
            }
            _equipmentDataRepository = equipmentDataRepository;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetLatestEquipmentData(int equipmentId)
        {
            var response = await _equipmentDataRepository.GetLatestEquipmentData(equipmentId);
            return Request.CreateResponse(response);
        }
    }
}
