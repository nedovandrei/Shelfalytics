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
    [Authorize]
    [RoutePrefix("api/Statistics")]
    public class StatisticsController : ApiController
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            if (statisticsService == null) throw new ArgumentNullException(nameof(statisticsService));

            _statisticsService = statisticsService;
        }

        [HttpPost]
        [Route("EquipmentOOS")]
        public async Task<HttpResponseMessage> GetEquipmentsOOSPercentage(int equipmentId, GlobalFilter filter)
        {
            var res = await _statisticsService.GetEquipmentOOS(equipmentId, filter);
            return Request.CreateResponse(res);
        }

        [HttpPost]
        [Route("TopSKUOOS")]
        public async Task<HttpResponseMessage> GetTopSkuOOS(GlobalFilter filter)
        {
            var res = await _statisticsService.GetTopSkuOOS(filter);
            return Request.CreateResponse(res);
        }

        [Route("ProductSalesSummary")]
        [HttpPost]
        public async Task<HttpResponseMessage> GetProductSalesSummary(GlobalFilter filter)
        {
            var res = await _statisticsService.GetProductSalesSummary(filter);
            return Request.CreateResponse(res);
        }

        [Route("ProductSales")]
        [HttpPost]
        public async Task<HttpResponseMessage> GetProductSalesData(int equipmentId, GlobalFilter filter)
        {
            var res = await _statisticsService.GetProductSalesData(equipmentId, filter);
            return Request.CreateResponse(res);
        }

        [HttpPost]
        [Route("POSOOS")]
        public async Task<HttpResponseMessage> GetPosOOS(int posId, GlobalFilter filter)
        {
            var res = await _statisticsService.GetPOSOOS(posId, filter);
            return Request.CreateResponse(res);
        }

        [HttpPost]
        [Route("posOOSsummary")]
        public async Task<HttpResponseMessage> GetPosOosSummary(GlobalFilter filter)
        {
            var res = await _statisticsService.GetPOSOOSSummary(filter);
            return Request.CreateResponse(res);
        }

        [HttpPost]
        [Route("equipmentLosses")]
        public async Task<HttpResponseMessage> GetEquipmentLossesDueToOOS(int equipmentId, GlobalFilter filter)
        {
            var res = await _statisticsService.GetEquipmentLossesDueToOos(equipmentId, filter);
            return Request.CreateResponse(res);
        }

        [HttpPost]
        [Route("lossesSummary")]
        public async Task<HttpResponseMessage> GetLossesDueToOOSSummary(GlobalFilter filter)
        {
            var res = await _statisticsService.GetLossesDueToOOSSummary(filter);
            return Request.CreateResponse(res);
        }
    }
}
