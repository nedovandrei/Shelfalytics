using System.Data.Entity;

namespace Shelfalytics.RepositoryInterface
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork GetShelfalyticsDbContext();
        DbContext GetShelfalyticsIdentityDbContext();
        IUnitOfWork GetSShelfIntegrationDbContext();
    }
}
