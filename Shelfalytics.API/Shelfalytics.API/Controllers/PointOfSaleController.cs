using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Shelfalytics.ServiceInterface;
using Shelfalytics.RepositoryInterface.Helpers;

namespace Shelfalytics.API.Controllers
{
    [Authorize]
    public class PointOfSaleController : ApiController
    {
        private readonly IPointOfSaleService _pointOfSaleService;

        public PointOfSaleController(IPointOfSaleService pointOfSaleService)
        {
            if (pointOfSaleService == null) throw new ArgumentNullException(nameof(pointOfSaleService));

            _pointOfSaleService = pointOfSaleService;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetPointOfSaleData(int posId, int clientId)
        {
            var res = await _pointOfSaleService.GetPointOfSaleData(posId, clientId);

            return Request.CreateResponse(res);
        }

        [HttpPost]
        [Route("api/PointOfSale/GetPointsOfSales")]
        public async Task<HttpResponseMessage> GetPointsOfSales(GlobalFilter filter)
        {
            var res = await _pointOfSaleService.GetPointsOfSales(filter);

            return Request.CreateResponse(res);
        }
    }
}
