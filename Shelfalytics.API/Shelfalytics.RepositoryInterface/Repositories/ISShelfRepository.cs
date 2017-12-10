using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfalytics.RepositoryInterface.Repositories
{
    public interface ISShelfRepository
    {
        Task Authorize();
    }
}
