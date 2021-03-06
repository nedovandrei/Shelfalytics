﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shelfalytics.RepositoryInterface.DTO;
using Shelfalytics.RepositoryInterface.Repositories;
using Shelfalytics.ServiceInterface;
using Shelfalytics.RepositoryInterface.Helpers;
using System.Linq;

namespace Shelfalytics.Service
{
    public class PointOfSaleService : IPointOfSaleService
    {
        private readonly IPointOfSaleRepository _pointOfSaleRepository;
        private readonly IEquipmentDataRepository _equipmentDataRepository;

        public PointOfSaleService(IPointOfSaleRepository pointOfSaleRepository, IEquipmentDataRepository equipmentDataRepository)
        {
            if(pointOfSaleRepository == null) throw new ArgumentNullException(nameof(pointOfSaleRepository));
            if (equipmentDataRepository == null) throw new ArgumentNullException(nameof(equipmentDataRepository));

            _pointOfSaleRepository = pointOfSaleRepository;
            _equipmentDataRepository = equipmentDataRepository;
        }

        public async Task<IEnumerable<PointOfSaleDataDTO>> GetPointOfSaleData(int posId, int clientId)
        {
            var posData = await _pointOfSaleRepository.GetPointOfSaleData(posId);
            var equipmentIds = await _equipmentDataRepository.GetPointOfSaleEquipment(posId, clientId);

            foreach (var pos in posData)
            {
                pos.EquipmentIds = equipmentIds;
            }

            return posData;
        }

        public async Task<IEnumerable<PointOfSaleDataDTO>> GetPointsOfSales(GlobalFilter filter)
        {
            var posData = await _pointOfSaleRepository.GetPointsOfSales(filter.ClientId, filter.IsAdmin);
            

            foreach (var pos in posData)
            {
                //var equipmentIds = await _equipmentDataRepository.GetPointOfSaleEquipment(pos.PointOfSaleId, filter.ClientId);
                var equipmentIds = pos.Equipment.Where(x => x.ClientId == filter.ClientId).Select(x => x.Id).ToList();
                pos.EquipmentIds = equipmentIds;
            }

            return posData;
        }
    }
}
