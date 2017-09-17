using Shelfalytics.RepositoryInterface.DTO;
using System.Threading.Tasks;

namespace Shelfalytics.ServiceInterface
{
    public interface IMailService
    {
        Task SendOOSEmail(ProductOOSDTO product, int equipmentId);
    }
}
