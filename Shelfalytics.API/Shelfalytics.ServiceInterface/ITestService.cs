using Shelfalytics.RepositoryInterface.Helpers;
using System.Threading.Tasks;

namespace Shelfalytics.ServiceInterface
{
    public interface ITestService
    {
        Task FillEquipmentWithFakeSaleData(int equipmentId, GlobalFilter timeSpan);
    }
}
