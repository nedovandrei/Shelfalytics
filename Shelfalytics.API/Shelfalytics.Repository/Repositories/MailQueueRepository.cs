using Shelfalytics.Model.DbModelHelpers;
using Shelfalytics.RepositoryInterface;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Shelfalytics.Repository.Repositories
{
    public class MailQueueRepository : IMailQueueRepository
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public MailQueueRepository(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null)
            {
                throw new ArgumentNullException(nameof(unitOfWorkFactory));
            }
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<IEnumerable<MailOosQueueDTO>> GetProductMailQueue(int equipmentId, int productId)
        {
            var timeLimit = DateTime.UtcNow.AddHours(-1);

            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                await DeleteOutdatedQueues(uow, 1);

                var query = from q in uow.Set<MailOosQueue>()
                            where
                                q.EquipmentId == equipmentId &&
                                q.ProductId == productId &&
                                q.TimeStamp > timeLimit
                            select new MailOosQueueDTO
                            {
                                ClientId = q.ClientId,
                                EquipmentId = q.EquipmentId,
                                ProductId = q.ProductId,
                                UserId = q.UserId,
                                TimeStamp = q.TimeStamp
                            };
                return await query.ToListAsync();

            }
        }

        public async Task AddToQueue(MailOosQueueDTO mailQueue)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                //await DeleteOutdatedQueues(uow, 1); // Unneeded for now.

                var queue = new MailOosQueue
                {
                    ClientId = mailQueue.ClientId,
                    EquipmentId = mailQueue.EquipmentId,
                    ProductId = mailQueue.ProductId,
                    UserId = mailQueue.UserId,
                    TimeStamp = DateTime.UtcNow
                };
                uow.Add(queue);
                await uow.CommitAsync();
            }
        }

        private async Task DeleteOutdatedQueues(IUnitOfWork uow, int timeSpanHours)
        {
            var timeSpan = DateTime.UtcNow.AddHours(-timeSpanHours);
            var result = await uow.Set<MailOosQueue>().Where(x => x.TimeStamp <= timeSpan).ToListAsync();
            if (result.Count() > 0)
            {
                uow.RemoveRange(result);
                await uow.CommitAsync();
            }
        }
    }
}
