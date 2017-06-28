﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Shelfalytics.Model.DbModels;
using Shelfalytics.RepositoryInterface;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Repositories;

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
                        Row = pl.Row,
                        ProductName = product.TradeMark,
                        SKUName = product.SKUName
                    };
                return await query.ToListAsync();
            }
        }
    }
}