using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shelfalytics.Model.DbModels.SShelfIntegration;
using Shelfalytics.RepositoryInterface;

namespace Shelfalytics.DbContext
{
    public class SShelfIntegrationDbContext : System.Data.Entity.DbContext, IUnitOfWork
    {
        public SShelfIntegrationDbContext() : base("SShelfIntegrationDB") { }

        public DbSet<SShelfEquipmentReading> EquipmentReadings { get; set; }
        public DbSet<SShelfEquipmentPusherReading> EquipmentPusherReadings { get; set; }
        public DbSet<SShelfEquipmentSalesReading> EquipmentSalesReadings { get; set; }

        public async Task CommitAsync()
        {
            await SaveChangesAsync();
        }

        public void Commit()
        {
            SaveChanges();
        }

        public new IQueryable<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public async Task<List<TEntity>> ToListAsync<TEntity>(IQueryable<TEntity> set) where TEntity : class
        {
            return await set.ToListAsync();
        }

        public async Task<int> CountAsync<TEntity>(IQueryable<TEntity> set)
        {
            return await set.CountAsync();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            base.Set<TEntity>().Add(entity);
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            base.Set<TEntity>().AddRange(entities);
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            AttachOnRemove(entity);
            base.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            var list = entities.ToList();
            list.ToList().ForEach(AttachOnRemove);
            base.Set<TEntity>().RemoveRange(list);
        }

        private void AttachOnRemove<TEntity>(TEntity entity) where TEntity : class
        {
            DbEntityEntry entry = Entry(entity);
            if (entry.State == EntityState.Detached)
                base.Set<TEntity>().Attach(entity);
        }
    }
}
