﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shelfalytics.Model.DbModels;
using Shelfalytics.RepositoryInterface;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Repositories;

namespace Shelfalytics.Repository.Repositories
{
    public class PointOfSaleRepository : IPointOfSaleRepository
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public PointOfSaleRepository(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null) throw new ArgumentNullException(nameof(unitOfWorkFactory));

            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<IEnumerable<PointOfSaleDataDTO>> GetPointOfSaleData(int posId)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = from pos in uow.Set<PointOfSale>()
                    where pos.Id == posId
                    select new PointOfSaleDataDTO
                    {
                        PointOfSaleId = pos.Id,
                        PointOfSaleAddress = pos.Address,
                        PointOfSaleTelephone = pos.Telephone,
                        PointOfSaleName = pos.PointOfSaleName,
                        ContactPersonName = pos.ContactPersonName,
                        OpeningHours = pos.OpeningHour,
                        ClosingHours = pos.ClosingHour
                    };

                return await query.ToListAsync();
            }
        }

        public async Task<IEnumerable<PointOfSaleDataDTO>> GetPointsOfSales()
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = from pos in uow.Set<PointOfSale>()
                            join equipment in uow.Set<Equipment>() on pos.Id equals equipment.PointOfSaleId into equipmentArray
                            select new PointOfSaleDataDTO
                            {
                                PointOfSaleId = pos.Id,
                                PointOfSaleAddress = pos.Address,
                                PointOfSaleTelephone = pos.Telephone,
                                PointOfSaleName = pos.PointOfSaleName,
                                ContactPersonName = pos.ContactPersonName,
                                OpeningHours = pos.OpeningHour,
                                ClosingHours = pos.ClosingHour,
                                Equipment = equipmentArray
                            };
                return await query.ToListAsync();
            }
        }
    }
}