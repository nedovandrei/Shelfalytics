using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Primitives;
using Shelfalytics.Model.DbModels;
using Shelfalytics.Repository;
using Shelfalytics.Repository.Repositories;
using Shelfalytics.RepositoryInterface.Repositories;

namespace Shelfalytics.API.Handlers
{
    public class CustomMessageHandler : DelegatingHandler
    {
        private readonly IExceptionLogRepository _exceptionLogRepository = new ExceptionLogRepository(new UnitOfWorkFactory());
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var result = await base.SendAsync(request, cancellationToken);

            if (result.StatusCode == HttpStatusCode.OK) return result;
            var log = new ExceptionLog
            {
                Type = "http_request_error",
                Exception = "Http Request Error",
                Request = request.ToString(),
                Response = result.ToString()
            };
            _exceptionLogRepository.SaveExceptionLog(log);

            return result;
        }
    }
}