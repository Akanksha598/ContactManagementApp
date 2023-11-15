namespace ContactManagementAPI.Repository;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();

    Task<T> GetAsync(bool tracked = true);

    Task CreateAsync(T entity);

    Task RemoveAsync(T entity);

    Task SaveAsync();
}
