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
                        product.TradeMark,
                        product.ShortSKUName
                    }
                    into set
                    group set by new
                    {
                        set.SKUName,
                        set.TradeMark,
                        set.ShortSKUName
                    }
                    into groupedSet
                    select new EquipmentProductSalesDTO
                    {
                        ProductName = groupedSet.Key.SKUName,
                        Sales = groupedSet.Where(x => x.SKUName == groupedSet.Key.SKUName).Sum(x => x.Quantity),
                        TradeMark = groupedSet.Key.TradeMark,
                        ShortProductName = groupedSet.Key.ShortSKUName
                    };

                return await query.ToListAsync();
            }
        }

        public async Task<IEnumerable<EquipmentProductSalesDTO>> GetProductSalesSummary(GlobalFilter filter)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = from equipment in uow.Set<Equipment>()
                            join sale in uow.Set<Sale>() on equipment.Id equals sale.EquipmentId
                            join product in uow.Set<Product>() on sale.ProductId equals product.Id
                            where sale.TimeStamp <= filter.EndTime && sale.TimeStamp >= filter.StartTime && (filter.IsAdmin ? true :  equipment.ClientId == filter.ClientId)
                    select new
                    {
                        sale.Quantity,
                        sale.TimeStamp,
                        product.SKUName,
                        product.TradeMark,
                        product.ShortSKUName
                    }
                    into set
                    group set by new
                    {
                        set.SKUName,
                        set.TradeMark,
                        set.ShortSKUName
                    }
                    into groupedSet
                    select new EquipmentProductSalesDTO
                    {
                        ProductName = groupedSet.Key.SKUName,
                        Sales = groupedSet.Where(x => x.SKUName == groupedSet.Key.SKUName).Sum(x => x.Quantity),
                        TradeMark = groupedSet.Key.TradeMark,
                        ShortProductName = groupedSet.Key.ShortSKUName
                    };

                return await query.ToListAsync();
            }
        }

        public async Task<IEnumerable<EquipmentProductSalesDTO>> GetProductSalesSummary(ExportFilter filter)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = from equipment in uow.Set<Equipment>()
                            join sale in uow.Set<Sale>() on equipment.Id equals sale.EquipmentId
                            join product in uow.Set<Product>() on sale.ProductId equals product.Id
                            where
                                sale.TimeStamp <= filter.EndTime && sale.TimeStamp >= filter.StartTime && 
                                (filter.IsAdmin ? true : equipment.ClientId == filter.ClientId) &&
                                filter.Equipments.Count() > 0 ? filter.Equipments.Contains(equipment.Id) : true &&
                                filter.Products.Count() > 0 ? filter.Products.Contains(equipment.Id) : true 
                            select new
                            {
                                product.Id,
                                sale.Quantity,
                                sale.TimeStamp,
                                product.SKUName,
                                product.TradeMark,
                                product.ShortSKUName
                            }
                    into set
                            group set by new
                            {
                                set.Id,
                                set.SKUName,
                                set.TradeMark,
                                set.ShortSKUName
                            }
                    into groupedSet
                            select new EquipmentProductSalesDTO
                            {
                                ProductId = groupedSet.Key.Id,
                                ProductName = groupedSet.Key.SKUName,
                                Sales = groupedSet.Where(x => x.Id == groupedSet.Key.Id).Sum(x => x.Quantity),
                                TradeMark = groupedSet.Key.TradeMark,
                                ShortProductName = groupedSet.Key.ShortSKUName
                            };

                return await query.ToListAsync();
            }
        }

        public async Task<IEnumerable<ProductSalesAverageDTO>> GetProductSalesAverage(int productId, GlobalFilter filter)
        {
            var timeSpanDateTime = filter.EndTime - filter.StartTime;
            var timeSpan = timeSpanDateTime.Days + (timeSpanDateTime.Hours / 24.0) + (timeSpanDateTime.Minutes / 3600.0);

            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = from sale in uow.Set<Sale>()
                            join product in uow.Set<Product>() on sale.ProductId equals product.Id
                            join equipment in uow.Set<Equipment>() on sale.EquipmentId equals equipment.Id
                            where sale.TimeStamp <= filter.EndTime && sale.TimeStamp >= filter.StartTime && sale.ProductId == productId && (filter.IsAdmin ? true : equipment.ClientId == filter.ClientId)
                            select new
                            {
                                sale.Quantity,
                                sale.TimeStamp,
                                product.SKUName,
                                product.TradeMark,
                                product.ShortSKUName
                            }
                    into set
                            group set by new
                            {
                                set.SKUName,
                                set.TradeMark,
                                set.ShortSKUName
                            }
                    into groupedSet
                            select new ProductSalesAverageDTO
                            {
                                ProductName = groupedSet.Key.SKUName,
                                AverageSales = timeSpan > 0 ? groupedSet.Where(x => x.SKUName == groupedSet.Key.SKUName).Sum(x => x.Quantity) / (decimal)timeSpan : 0,
                                TradeMark = groupedSet.Key.TradeMark,
                                ShortProductName = groupedSet.Key.ShortSKUName
                            };
                return await query.ToListAsync();
            }
        }
    }
}
