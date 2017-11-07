using Shelfalytics.RepositoryInterface.DTO.Export;
using Shelfalytics.RepositoryInterface.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.Repositories
{
    public interface IExportRepository
    {
        Task<IEnumerable<ProductOpensDTO>> GetProductOpenCount(ExportFilter filter);
    }
}
