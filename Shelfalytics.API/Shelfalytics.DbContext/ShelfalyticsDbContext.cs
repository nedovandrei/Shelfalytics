using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Shelfalytics.Model.DbModelHelpers;
using Shelfalytics.Model.DbModels;
using Shelfalytics.RepositoryInterface;

namespace Shelfalytics.DbContext
{
    public class ShelfalyticsDbContext : System.Data.Entity.DbContext, IUnitOfWork
    {
        public ShelfalyticsDbContext() : base("ShelfalyticsDevDB") { }

        //main models
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PointOfSale> PointOfSales { get; set; }
        

        //equipment
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentReading> EquipmentReadings { get; set; }
        public DbSet<EquipmentPlanogram> EquipmentPlanograms { get; set; }
        public DbSet<EquipmentDistanceReading> EquipmentDistanceReadings { get; set; }

        //helpers
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<PackagingType> PackagintTypes { get; set; }


        public async Task CommitAsync()
        {
            await SaveChangesAsync();
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
