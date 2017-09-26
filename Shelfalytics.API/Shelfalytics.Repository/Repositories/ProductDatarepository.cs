using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Shelfalytics.Model.DbModels;
using Shelfalytics.RepositoryInterface;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Repositories;
using Shelfalytics.RepositoryInterface.Helpers;

namespace Shelfalytics.Repository.Repositories
{
    public class ProductDataRepository : IProductDataRepository
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ProductDataRepository(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null) throw new ArgumentNullException(nameof(unitOfWorkFactory));

            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<IEnumerable<EquipmentPlanogramDTO>> GetEquipmentPlanogram(int equipmentId)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = from pl in uow.Set<EquipmentPlanogram>()
                    join product in uow.Set<Product>() on pl.ProductId equals product.Id
                    where pl.EquipmentId == equipmentId
                    select new EquipmentPlanogramDTO
                    {
                        ProductId = product.Id,
                        EquipmentId = equipmentId,
                        Row = pl.Row,
                        ProductName = product.TradeMark,
                        SKUName = product.SKUName,
                        ShortSKUName = product.ShortSKUName,
                        BottleDiameter = product.BottleDiameter,
                        Price = product.Price,
                        PhotoPath = product.PhotoPath
                    };
                return await query.ToListAsync();
            }
        }

        public async Task<IEnumerable<ProductDTO>> GetClientsProducts(int clientId, bool isAdmin)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = from eq in uow.Set<Equipment>()
                            join pl in uow.Set<EquipmentPlanogram>() on eq.Id equals pl.EquipmentId
                            join product in uow.Set<Product>() on pl.ProductId equals product.Id
                            where (isAdmin || eq.ClientId == clientId)
                            select new ProductDTO
                            {
                                Id = product.Id,
                                BottleDiameter = product.BottleDiameter,
                                EAN = product.EAN,
                                PackagingTypeId = product.PackagingTypeId,
                                PhotoPath = product.PhotoPath,
                                Price = product.Price,
                                ShortSKUName = product.ShortSKUName,
                                SKUName = product.SKUName,
                                TradeMark = product.TradeMark,
                                UnitOfMeasurement = product.UnitOfMeasurement,
                                Volume = product.Volume
                            };
                var result = await query.ToListAsync();
                return result.Distinct(new GenericCompare<ProductDTO>(x => x.Id)).ToList();
            }
        }

        public async Task<IEnumerable<ProductDTO>> GetClientsProducts(ExportFilter filter)
        {
            var productFilterCount = filter.Products.Count;

            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = from eq in uow.Set<Equipment>()
                            join pl in uow.Set<EquipmentPlanogram>() on eq.Id equals pl.EquipmentId
                            join product in uow.Set<Product>() on pl.ProductId equals product.Id
                            where (filter.IsAdmin || eq.ClientId == filter.ClientId) &&
                                (productFilterCount <= 0 || filter.Products.Contains(product.Id))
                            select new ProductDTO
                            {
                                Id = product.Id,
                                BottleDiameter = product.BottleDiameter,
                                EAN = product.EAN,
                                PackagingTypeId = product.PackagingTypeId,
                                PhotoPath = product.PhotoPath,
                                Price = product.Price,
                                ShortSKUName = product.ShortSKUName,
                                SKUName = product.SKUName,
                                TradeMark = product.TradeMark,
                                UnitOfMeasurement = product.UnitOfMeasurement,
                                Volume = product.Volume
                            };
                var result = await query.ToListAsync();
                return result.Distinct(new GenericCompare<ProductDTO>(x => x.Id)).ToList();
            }
        }
    }

    class GenericCompare<T> : IEqualityComparer<T> where T : class
    {
        private Func<T, object> _expr { get; set; }
        public GenericCompare(Func<T, object> expr)
        {
            this._expr = expr;
        }
        public bool Equals(T x, T y)
        {
            var first = _expr.Invoke(x);
            var sec = _expr.Invoke(y);
            if (first != null && first.Equals(sec))
                return true;
            else
                return false;
        }
        public int GetHashCode(T obj)
        {
            return _expr.Invoke(obj).GetHashCode();
        }
    }
}
