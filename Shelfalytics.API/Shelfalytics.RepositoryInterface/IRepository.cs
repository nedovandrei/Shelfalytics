using System.Collections.Generic;
using System.Linq;

namespace Shelfalytics.RepositoryInterface
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        void Create(T data);
        void Update(T data);
        void Delete(T data);
        void AddRange(IEnumerable<T> data);

    }
}
