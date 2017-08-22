﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Shelfalytics.Model.DbModels;
using Shelfalytics.RepositoryInterface;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Helpers;
using Shelfalytics.RepositoryInterface.Repositories;

namespace Shelfalytics.Repository.Repositories
{
    public class EquipmentDataRepository : IEquipmentDataRepository
    {
        
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public EquipmentDataRepository(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null)
            {
                throw new ArgumentNullException(nameof(unitOfWorkFactory));
            }
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<IEnumerable<EqiupmentDataDTO>> GetLatestEquipmentData(int equipmentId)
        {
            var today = DateTime.Today;
            var tomorrow = DateTime.Today.AddDays(1);
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = (from equipment in uow.Set<Equipment>()
                    join equipmentReading in uow.Set<EquipmentReading>() on equipment.Id equals
                    equipmentReading.EquipmentId
                             join er in uow.Set<EquipmentReading>() on equipment.Id equals
                             er.EquipmentId into ers
                    join distanceReading in uow.Set<EquipmentDistanceReading>() on equipmentReading.Id equals distanceReading.EquipmentReadingId into distanceReadings
                    where equipment.Id == equipmentId
                    orderby equipmentReading.TimeSpamp descending
                    select new EqiupmentDataDTO
                    {
                        Id = equipment.Id,
                        ClientName = equipment.Client.ClientName,
                        EquipmentType = equipment.EquipmentType.Name,
                        ModelName = equipment.ModelName,
                        PointOfSaleName = equipment.PointOfSale.PointOfSaleName,
                        PointOfSaleAddress = equipment.PointOfSale.Address,
                        PointOfSaleTelephone = equipment.PointOfSale.Telephone,
                        OpenCloseCountToday = ers.Count(x => x.WasOpened && x.TimeSpamp > today && x.TimeSpamp < tomorrow),
                        Temperature = equipmentReading.Temperature,
                        TimeStamp = equipmentReading.TimeSpamp,
                        RowCount = equipment.RowCount,
                        DistanceReadings = distanceReadings,
                        EmptyDistance = equipment.EmptyDistance,
                        FullDistance = equipment.FullDistance,
                        Width = equipment.Width,
                        YCount = equipment.YCount
                    }).Take(1);
                return await query.ToListAsync();
            }
        }

        public async Task<IEnumerable<int>> GetPointOfSaleEquipment(int posId)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = from eq in uow.Set<Equipment>()
                    where eq.PointOfSaleId == posId
                    //&& (eq.UserId == userId) TODO: implement user 
                    select eq.Id;

                return await query.ToListAsync();
            }
        }

        public async Task<IEnumerable<EquipmentDTO>> GetEquipments()
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = from equipment in uow.Set<Equipment>()
                    select new EquipmentDTO
                    {
                        Id = equipment.Id,
                        PointOfSaleId = equipment.PointOfSaleId,
                        RowCount = equipment.RowCount,
                        IMEI = equipment.IMEI,
                        ClientId = equipment.ClientId,
                        ModelName = equipment.ModelName,
                        EquipmentTypeId = equipment.EquipmentTypeId,
                        YCount = equipment.YCount,
                        FullDistance = equipment.FullDistance,
                        EmptyDistance = equipment.EmptyDistance
                    };
                return await query.ToListAsync();
            }
        }

        public async Task<EquipmentDTO> GetEquipmentById(int equipmentId)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = from equipment in uow.Set<Equipment>()
                            where equipment.Id == equipmentId
                            select new EquipmentDTO
                            {
                                Id = equipment.Id,
                                PointOfSaleId = equipment.PointOfSaleId,
                                RowCount = equipment.RowCount,
                                IMEI = equipment.IMEI,
                                ClientId = equipment.ClientId,
                                ModelName = equipment.ModelName,
                                EquipmentTypeId = equipment.EquipmentTypeId,
                                Width = equipment.Width,
                                YCount = equipment.YCount,
                                FullDistance = equipment.FullDistance,
                                EmptyDistance = equipment.EmptyDistance
                            };
                return await query.FirstOrDefaultAsync();
            }
        }

        public async Task<IEnumerable<EqiupmentDataDTO>> GetFilteredEquipmentData(int equipmentId, GlobalFilter filter)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = from equipment in uow.Set<Equipment>()
                    join equipmentReading in uow.Set<EquipmentReading>() on equipment.Id equals
                    equipmentReading.EquipmentId
                    join equipmentDistanceReading in uow.Set<EquipmentDistanceReading>() on equipmentReading.Id equals
                    equipmentDistanceReading.EquipmentReadingId into distanceReadings
                    where
                    equipment.Id == equipmentId && equipmentReading.TimeSpamp >= filter.StartTime &&
                    equipmentReading.TimeSpamp <= filter.EndTime
                    select new EqiupmentDataDTO
                    {
                        Id = equipment.Id,
                        RowCount = equipment.RowCount,
                        ClientName = equipment.Client.ClientName,
                        EquipmentType = equipment.EquipmentType.Name,
                        ModelName = equipment.ModelName,
                        PointOfSaleName = equipment.PointOfSale.PointOfSaleName,
                        PointOfSaleAddress = equipment.PointOfSale.Address,
                        PointOfSaleTelephone = equipment.PointOfSale.Telephone,
                        Temperature = equipmentReading.Temperature,
                        TimeStamp = equipmentReading.TimeSpamp,
                        DistanceReadings = distanceReadings,
                        Width = equipment.Width,
                        YCount = equipment.YCount
                    };
                return await query.ToListAsync();

            }
        }

        public async Task<EquipmentDTO> GetEquipmentByIMEI(string imei)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = from st in uow.Set<Equipment>()
                    where st.IMEI.Contains(imei)
                    select new EquipmentDTO
                    {
                        Id = st.Id,
                        IMEI = st.IMEI,
                        PointOfSaleId = st.PointOfSaleId,
                        ClientId = st.ClientId,
                        ModelName = st.ModelName,
                        RowCount = st.RowCount
                    };
                return await query.FirstAsync();
            }
        }

        public async Task<EquipmentReading> RegisterEquipmentReading(EquipmentReading reading)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                uow.Add(reading);
                await uow.CommitAsync();
            }

            return reading;
        }

        public async Task<EquipmentReadingGetDTO> GetLatestReading(int equipmentId)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = from er in uow.Set<EquipmentReading>()
                    join edr in uow.Set<EquipmentDistanceReading>() on er.Id equals edr.EquipmentReadingId into edrSet
                    where er.EquipmentId == equipmentId
                    orderby er.Id descending
                    select new EquipmentReadingGetDTO
                    {
                        Id = er.Id,
                        TimeSpamp = er.TimeSpamp,
                        SensorReadings = edrSet,
                        Temperature = er.Temperature
                    };
                return await query.FirstAsync();
            }
        }

        public async Task RegisterEquipmentDistanceReadings(IEnumerable<EquipmentDistanceReading> distanceReadings)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                uow.AddRange(distanceReadings);
                await uow.CommitAsync();
            }
        }

        public async Task<bool> EquipmentHasReadings(int equipmentId)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                return await uow.Set<EquipmentReading>().AnyAsync(x => x.EquipmentId == equipmentId);
            }
        }

    }
}
