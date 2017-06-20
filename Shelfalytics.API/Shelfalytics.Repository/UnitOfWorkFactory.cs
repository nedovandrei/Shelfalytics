using Shelfalytics.DbContext;
using Shelfalytics.RepositoryInterface;

namespace Shelfalytics.Repository
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork GetShelfalyticsDbContext()
        {
            return new ShelfalyticsDbContext();
        }
    }
}
