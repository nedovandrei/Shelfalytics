using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
        IQueryable<TEntity> Set<TEntity>() where TEntity : class;
        Task<List<TEntity>> ToListAsync<TEntity>(IQueryable<TEntity> set) where TEntity : class;
        Task<int> CountAsync<TEntity>(IQueryable<TEntity> set);

        void Add<TEntity>(TEntity entity) where TEntity : class;
        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void Remove<TEntity>(TEntity entity) where TEntity : class;
        void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    }
}
