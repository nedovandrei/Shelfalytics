using Shelfalytics.RepositoryInterface.DTO.Export;
using Shelfalytics.RepositoryInterface.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.ServiceInterface
{
    public interface IExportService
    {
        Task<ExportSelects> GetFilterSelectsData(GlobalFilter filter);
        Task<byte[]> GenerateExcelReport(ExportFilter filter);
    }
}
