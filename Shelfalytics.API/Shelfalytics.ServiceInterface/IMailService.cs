using Shelfalytics.RepositoryInterface.DTO;
using System.Threading.Tasks;

namespace Shelfalytics.ServiceInterface
{
    public interface IMailService
    {
        Task SendOOSEmail(ProductOOSDTO product, int equipmentId);
        Task TestSendOOSEmail(ProductOOSDTO product, int equipmentId, string to);
    }
}
