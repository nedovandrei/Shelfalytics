using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using Shelfalytics.Model.DbModels;
using Shelfalytics.Repository;
using Shelfalytics.Repository.Repositories;
using Shelfalytics.RepositoryInterface.Repositories;

namespace Shelfalytics.API.Handlers
{
    public class CustomExceptionLogger : ExceptionLogger
    {
        private readonly IExceptionLogRepository _exceptionLogRepository = new ExceptionLogRepository(new UnitOfWorkFactory());

        public override void Log(ExceptionLoggerContext context)
        {
            var log = context.Exception.ToString();
            var log2 = context.Request.ToString();

            var exceptionLog = new ExceptionLog
            {
                Type = "inner_exception",
                Exception = context.Exception.ToString(),
                Request = context.Request.ToString()
            };

            _exceptionLogRepository.SaveExceptionLog(exceptionLog);

            //Do whatever logging you need to do here.
            //File.WriteAllText("D:\\exceptionlog.txt", log + "\n\n" + log2);
        }
    }
}