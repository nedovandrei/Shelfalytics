using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shelfalytics.Model.DbModels;
using Shelfalytics.RepositoryInterface;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Helpers;
using Shelfalytics.RepositoryInterface.Repositories;

namespace Shelfalytics.Repository.Repositories
{
    public class ExceptionLogRepository : IExceptionLogRepository
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ExceptionLogRepository(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null) throw new ArgumentNullException(nameof(unitOfWorkFactory));
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<IEnumerable<ExceptionLogDTO>> GetExceptionLogs()
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = from st in uow.Set<ExceptionLog>()
                    select new ExceptionLogDTO
                    {
                        Id = st.Id,
                        Exception = st.Exception,
                        Request = st.Request,
                        Response = st.Response,
                        TimeStamp = st.TimeStamp,
                        Type = st.Type
                    };
                return await query.ToListAsync();
            }
        }

        public async Task<IEnumerable<ExceptionLogDTO>> GetExceptionLogs(GlobalFilter filter)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = from st in uow.Set<ExceptionLog>()
                            where st.TimeStamp >= filter.StartTime && st.TimeStamp <= filter.EndTime
                            select new ExceptionLogDTO
                            {
                                Id = st.Id,
                                Exception = st.Exception,
                                Request = st.Request,
                                Response = st.Response,
                                TimeStamp = st.TimeStamp,
                                Type = st.Type
                            };
                return await query.ToListAsync();
            }
        }

        public async Task<IEnumerable<ExceptionLogDTO>> GetExceptionLogById(int id)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = (from st in uow.Set<ExceptionLog>()
                    where st.Id == id
                    select new ExceptionLogDTO
                    {
                        Id = st.Id,
                        Exception = st.Exception,
                        Request = st.Request,
                        Response = st.Response,
                        TimeStamp = st.TimeStamp,
                        Type = st.Type
                    }).Take(1);
                return await query.ToListAsync();
            }
        }

        public void SaveExceptionLog(ExceptionLog log)
        {
            log.TimeStamp = DateTime.Now;
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                uow.Add(log);
                uow.Commit();
            }
        }

        public async Task DeleteExceptionLog(int id)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var item = await uow.Set<ExceptionLog>().FirstAsync(x => x.Id == id);
                uow.Remove(item);
                await uow.CommitAsync();
            }
        }

        public async Task DeleteAll()
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var logs = await uow.Set<ExceptionLog>().ToListAsync();
                uow.RemoveRange(logs);
                await uow.CommitAsync();
            }
        }
    }
}
