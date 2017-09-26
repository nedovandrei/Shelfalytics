using Shelfalytics.RepositoryInterface.Helpers;
using Shelfalytics.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace Shelfalytics.API.Controllers
{
    //[Authorize]
    [RoutePrefix("api/export")]
    public class ExportController : ApiController
    {
        private readonly IExportService _exportService;

        public ExportController(IExportService exportService)
        {
            if (exportService == null)
            {
                throw new ArgumentNullException(nameof(exportService));
            }

            _exportService = exportService;
        }

        [HttpPost]
        [Route("selects")]
        public async Task<HttpResponseMessage> GetFilterSelects(GlobalFilter filter)
        {
            var result = await _exportService.GetFilterSelectsData(filter);
            return Request.CreateResponse(result);
        }

        [Route("excel")]
        [HttpPost]
        public async Task<HttpResponseMessage> GetExcel(ExportFilter filter)
        {
            var result = await _exportService.GenerateExcelReport(filter);

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(result);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "ex" + filter.StartTime.ToShortDateString() + "-" + filter.EndTime.ToShortDateString() + ".xlsx";
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/ms-excel");
            response.Content.Headers.ContentLength = result.Length;
            response.StatusCode = HttpStatusCode.OK;

            return response;
        }

        [Route("exceltest")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetExcel()
        {
            var filter = new ExportFilter
            {
                StartTime = DateTime.UtcNow.AddMonths(-1),
                EndTime = DateTime.UtcNow,
                ChainNames = new List<string>(),
                Cities = new List<string>(),
                ClientId = 1,
                Equipments = new List<int>(),
                IsAdmin = false,
                PointsOfSale = new List<int>(),
                Products = new List<int>(),
                TradeChannels = new List<string>()
            };

            var result = await _exportService.GenerateExcelReport(filter);

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(result);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "export" + filter.StartTime.ToShortDateString() + "-" + filter.EndTime.ToShortDateString() + ".xlsx";
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/ms-excel");
            response.Content.Headers.ContentLength = result.Length;
            response.StatusCode = HttpStatusCode.OK;

            return response;
        }
    }
}
