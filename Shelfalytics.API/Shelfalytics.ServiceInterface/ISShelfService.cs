using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shelfalytics.RepositoryInterface.DTO.SShelfIntegration;

namespace Shelfalytics.ServiceInterface
{
    public interface ISShelfService
    {
        Task Test();
        Task RegisterReading(SShelfEquipmentReadingDTO reading);
    }
}
