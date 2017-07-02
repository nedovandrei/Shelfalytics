using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Shelfalytics.RepositoryInterface.Helpers;
using Shelfalytics.ServiceInterface;

namespace Shelfalytics.API.Controllers
{
    public class StatisticsController : ApiController
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            if (statisticsService == null) throw new ArgumentNullException(nameof(statisticsService));

            _statisticsService = statisticsService;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> GetEquipmentsOOSPercentage(int equipmentId, GlobalFilter filter)
        {
            var res = await _statisticsService.GetEquipmentOOS(equipmentId, filter);
            return Request.CreateResponse(res);
        }
    }
}
