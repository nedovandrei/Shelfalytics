using Shelfalytics.RepositoryInterface.Repositories;
using Shelfalytics.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shelfalytics.RepositoryInterface.DTO.SShelfIntegration;

namespace Shelfalytics.Service
{
    public class SShelfService : ISShelfService
    {
        private readonly ISShelfRepository _sShelfRepository;

        public SShelfService(ISShelfRepository sShelfRepository)
        {
            if (sShelfRepository == null)
            {
                throw new ArgumentNullException(nameof(sShelfRepository));
            }

            _sShelfRepository = sShelfRepository;
        }

        public async Task Test()
        {
            var responseStores = await _sShelfRepository.GetSShelfStores();
            var responseEquipment = await _sShelfRepository.GetSShelfEquipment();
            var responseProducts = await _sShelfRepository.GetSShelfProducts();


        }

        public async Task RegisterReading(SShelfEquipmentReadingDTO reading)
        {
            await _sShelfRepository.RegisterReading(reading);
        }
    }
}
