using System.Linq.Expressions;

namespace ContactManagementAPI.Repository;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();

    Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true);

    Task CreateAsync(T entity);

    Task RemoveAsync(T entity);

    Task SaveAsync();
}
