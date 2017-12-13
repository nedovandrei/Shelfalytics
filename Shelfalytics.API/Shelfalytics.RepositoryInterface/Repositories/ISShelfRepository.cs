using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shelfalytics.RepositoryInterface.DTO.SShelfIntegration;

namespace Shelfalytics.RepositoryInterface.Repositories
{
    public interface ISShelfRepository
    {
        Task Authorize();
        Task RegisterReading(SShelfEquipmentReadingDTO reading);
    }
}
