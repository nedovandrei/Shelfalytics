using Shelfalytics.RepositoryInterface.Repositories;
using Shelfalytics.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            await _sShelfRepository.Authorize();
        }
    }
}
