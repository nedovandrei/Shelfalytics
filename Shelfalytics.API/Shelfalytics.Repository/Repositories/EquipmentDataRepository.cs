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

namespace Shelfalytics.Repository.Repositories
{
    public class EquipmentDataRepository : IEquipmentDataRepository
    {
        private readonly int _fullStockDistance = 85;
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
            using (var uow = _unitOfWorkFactory.GetShelfalyticsDbContext())
            {
                var query = from equipment in uow.Set<Equipment>()
                    join equipmentReading in uow.Set<EquipmentReading>() on equipment.Id equals
                    equipmentReading.EquipmentId
                    where equipment.Id == equipmentId
                    select new EqiupmentDataDTO
                    {
                        Id = equipment.Id,
                        ClientName = equipment.Client.ClientName,
                        EquipmentType = equipment.EquipmentType.Name,
                        ModelName = equipment.ModelName,
                        Planogram = equipment.Planogram,
                        OpenCloseCount = equipmentReading.OpenCloseCount,
                        PercentageLine1 = equipmentReading.Distance1 / _fullStockDistance * 100,
                        PercentageLine2 = equipmentReading.Distance2 / _fullStockDistance * 100,
                        PercentageLine3 = equipmentReading.Distance3 / _fullStockDistance * 100
                    };
                return await query.ToListAsync();
            }
        }
    }
}
