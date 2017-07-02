using System;
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
                        DistanceReadings = distanceReadings
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
                    };
                return await query.ToListAsync();

            }
        }
        

    }
}
