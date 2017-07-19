using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shelfalytics.Model.DbModels;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Helpers;

namespace Shelfalytics.RepositoryInterface.Repositories
{
    public interface IExceptionLogRepository
    {
        Task<IEnumerable<ExceptionLogDTO>> GetExceptionLogs();
        Task<IEnumerable<ExceptionLogDTO>> GetExceptionLogs(GlobalFilter filter);
        Task<IEnumerable<ExceptionLogDTO>> GetExceptionLogById(int id); 
        void SaveExceptionLog(ExceptionLog log);
        Task DeleteExceptionLog(int id);
        Task DeleteAll();
    }
}
