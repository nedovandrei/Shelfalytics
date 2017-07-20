using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Shelfalytics.Model.DbModels;
using Shelfalytics.RepositoryInterface.Helpers;
using Shelfalytics.RepositoryInterface.Repositories;

namespace Shelfalytics.API.Controllers
{
    [RoutePrefix("api/ExceptionLog")]
    public class ExceptionLogController : ApiController
    {
        private readonly IExceptionLogRepository _exceptionLogRepository;

        public ExceptionLogController(IExceptionLogRepository exceptionLogRepository)
        {
            if (exceptionLogRepository == null) throw new ArgumentNullException(nameof(exceptionLogRepository));
            _exceptionLogRepository = exceptionLogRepository;
        }

        [HttpGet]
        [Route("all")]
        public async Task<HttpResponseMessage> GetExceptionLogs()
        {
            var result = await _exceptionLogRepository.GetExceptionLogs();
            return Request.CreateResponse(result);
        }

        [HttpPost]
        [Route("filtered")]
        public async Task<HttpResponseMessage> GetFilteredExceptionLogs(GlobalFilter filter)
        {
            var result = await _exceptionLogRepository.GetExceptionLogs(filter);
            return Request.CreateResponse(result);
        }

        [HttpGet]
        [Route("byid")]
        public async Task<HttpResponseMessage> GetExceptionLogById(int exceptionId)
        {
            var result = await _exceptionLogRepository.GetExceptionLogById(exceptionId);
            return Request.CreateResponse(result);
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteLog(int exceptionId)
        {
            await _exceptionLogRepository.DeleteExceptionLog(exceptionId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("deleteAll")]
        public async Task<HttpResponseMessage> DeleteAllLogs()
        {
            await _exceptionLogRepository.DeleteAll();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
