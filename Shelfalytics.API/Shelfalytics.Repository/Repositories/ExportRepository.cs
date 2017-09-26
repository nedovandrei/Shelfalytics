using Shelfalytics.Model.DbModelHelpers;
using Shelfalytics.Model.DbModels;
using Shelfalytics.RepositoryInterface;
using Shelfalytics.RepositoryInterface.DTO.Export;
using Shelfalytics.RepositoryInterface.Helpers;
using Shelfalytics.RepositoryInterface.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.Repository.Repositories
{
    public class ExportRepository : IExportRepository
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ExportRepository(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null)
            {
                throw new ArgumentNullException(nameof(unitOfWorkFactory));
            }

            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<IEnumerable<ProductOpensDTO>> GetProductOpenCount(ExportFilter filter)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = from eq in uow.Set<Equipment>()
                            join eqRead in uow.Set<EquipmentReading>() on eq.Id equals eqRead.EquipmentId
                            join plan in uow.Set<EquipmentPlanogram>() on eq.Id equals plan.EquipmentId
                            join product in uow.Set<Product>() on plan.ProductId equals product.Id
                            where
                                filter.Products.Count() > 0 ? filter.Products.Contains(product.Id) : true &&
                                filter.Equipments.Count() > 0 ? filter.Equipments.Contains(eq.Id) : true &&
                                filter.IsAdmin ? true : filter.ClientId == eq.ClientId &&
                                eqRead.WasOpened
                            select new
                            {
                                eq.Id,
                                ProductId = product.Id,
                                eqRead.WasOpened
                            } into set
                            group set by new
                            {
                                set.ProductId
                            } into groupedSet
                            select new ProductOpensDTO
                            {
                                ProductId = groupedSet.Key.ProductId,
                                OpenCount = groupedSet.Count()
                            };
                return await query.ToListAsync();
            }
        }

       

    }
}
