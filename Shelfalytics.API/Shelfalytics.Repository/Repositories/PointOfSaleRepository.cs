using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shelfalytics.Model.DbModels;
using Shelfalytics.RepositoryInterface;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Repositories;
using Shelfalytics.RepositoryInterface.Helpers;

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
                        ClosingHours = pos.ClosingHour,
                        Latitude = pos.Latitude,
                        Longitude = pos.Longitude
                    };

                return await query.ToListAsync();
            }
        }

        public async Task<IEnumerable<PointOfSaleDataDTO>> GetPointsOfSales(int clientId, bool isAdmin)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = from pos in uow.Set<PointOfSale>()
                            join equipment in uow.Set<Equipment>() on pos.Id equals equipment.PointOfSaleId into equipmentArray
                            where (isAdmin ? true : equipmentArray.Any(x => x.ClientId == clientId))
                            select new PointOfSaleDataDTO
                            {
                                PointOfSaleId = pos.Id,
                                PointOfSaleAddress = pos.Address,
                                PointOfSaleTelephone = pos.Telephone,
                                PointOfSaleName = pos.PointOfSaleName,
                                ChainName = pos.ChainName,
                                City = pos.City,
                                TradeChannel = pos.TradeChannel,
                                ContactPersonName = pos.ContactPersonName,
                                OpeningHours = pos.OpeningHour,
                                ClosingHours = pos.ClosingHour,
                                Equipment = equipmentArray,
                                Latitude = pos.Latitude,
                                Longitude = pos.Longitude
                            };
                return await query.ToListAsync();
            }
        }

        public async Task<IEnumerable<int>> GetPosEquipment(int posId, GlobalFilter filter)
        {
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = from eq in uow.Set<Equipment>()
                    where eq.PointOfSaleId == posId && (filter.IsAdmin ? true : eq.ClientId == filter.ClientId)
                    select eq.Id;

                return await query.ToListAsync();
            }
        }
    }
}
