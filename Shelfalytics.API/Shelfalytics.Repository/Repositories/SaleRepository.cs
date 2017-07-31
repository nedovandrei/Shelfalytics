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
    public class SaleRepository: ISaleRepository
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public SaleRepository(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null) throw new ArgumentNullException(nameof(unitOfWorkFactory));
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task RegisterSale(Sale sale)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                uow.Add(sale);
                await uow.CommitAsync();
            }
        }

        public async Task<IEnumerable<EquipmentProductSalesDTO>> GetEquipmentSales(int equipmentId, GlobalFilter filter)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = from sale in uow.Set<Sale>()
                    join product in uow.Set<Product>() on sale.ProductId equals product.Id
                    where sale.TimeStamp <= filter.EndTime && sale.TimeStamp >= filter.StartTime && sale.EquipmentId == equipmentId
                    select new
                    {
                        sale.Quantity,
                        sale.TimeStamp,
                        product.SKUName,
                        product.TradeMark
                    }
                    into set
                    group set by new
                    {
                        set.SKUName,
                        set.TradeMark
                    }
                    into groupedSet
                    select new EquipmentProductSalesDTO
                    {
                        ProductName = groupedSet.Key.SKUName,
                        Sales = groupedSet.Where(x => x.SKUName == groupedSet.Key.SKUName).Sum(x => x.Quantity),
                        TradeMark = groupedSet.Key.TradeMark
                    };

                return await query.ToListAsync();
            }
        }
    }
}
