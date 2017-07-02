using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shelfalytics.RepositoryInterface.Helpers;

namespace Shelfalytics.ServiceInterface
{
    public interface IStatisticsService
    {
        Task<double> GetEquipmentOOS(int equipmentId, GlobalFilter filter);
    }
}
