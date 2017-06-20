namespace Shelfalytics.RepositoryInterface
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork GetShelfalyticsDbContext();
    }
}
